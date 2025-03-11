using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Domain.Interfaces;
using API.Infrastructure.Repository;
using API.Domain.Entities;
using API.Application.DTOs;
using BCrypt.Net;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepository<CreateAccount> _repository;
        private readonly ICreateAccount<CreateAccount> _account;

        public AccountController(IRepository<CreateAccount> repository ,ICreateAccount<CreateAccount> account)
        {
            _repository = repository;
            _account = account;
        }

        [HttpGet]
        public ActionResult Getdata() {

            var obj = _repository.GetAll();

            if (obj == null || !obj.Any())
            {
                return NotFound("No se encontraron cuentas.");
            }

            return Ok(obj);
        }

        [HttpGet("GetUsers")]
        public ActionResult GetUsers()
        {
            try
            {
                var obj = _account.GetDataNameId();

                if (obj == null || !obj.Any())
                {
                    return NotFound("No se encontraron cuentas.");
                }

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



        [HttpGet("{id}")]
        public ActionResult GetByIdAccount(int id)
        {
        try
        {
            var obj = _repository.GetById(id);

            if (obj == null)
            {
                NotFound(new { message = $"No se encontró un usuario con ID {id}." });
            }
            return Ok(new { 
            obj.Id,
            obj.Name,
            obj.Email,
            Role = obj.Role.ToString()
            });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocurrió un error interno en el servidor.", error = ex.Message });
        }
    }

        [HttpPost]
        public ActionResult CreateAccountPost([FromBody] CreateAccountDTO createAccount)
        {
            if (createAccount == null)
            {
                return BadRequest(new { message = "El objeto no puede ser nulo" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAccount = _account.GetByCorreo(createAccount.Email);
            if (existingAccount != null)
            {
                return BadRequest(new { message = "El correo ya existe en el sistema." });
            }

            // ✅ Validamos `ConfirmPassword` antes de llegar aquí, ya no es necesario guardarlo
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createAccount.Password);

            var newAccount = new CreateAccount
            {
                Name = createAccount.Name,
                Email = createAccount.Email,
                Password = hashedPassword, // ✅ Solo almacenamos la versión encriptada
                Role = UserRole.User
            };

            _repository.Add(newAccount);

            return CreatedAtAction(nameof(CreateAccountPost), new { id = newAccount.Id }, new
            {
                newAccount.Id,
                newAccount.Name,
                newAccount.Email,
                newAccount.Role
            });
        }

    }
}
