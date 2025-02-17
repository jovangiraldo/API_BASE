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
            return Ok(obj);

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

            var exitingaccount = _account.GetByCorreo(createAccount.Email);

            if (exitingaccount != null)
            {

                return BadRequest(new { message = "El correo ya existe en el sistema." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createAccount.Password);
            var hashedConfirmPassword = BCrypt.Net.BCrypt.HashPassword(createAccount.ConfirmPassword);

            var newAcount = new CreateAccount
            {
                Name = createAccount.Name,
                Email = createAccount.Email,
                Password = hashedPassword,
                ConfirmPassword = hashedConfirmPassword,
            };

            _repository.Add(newAcount);

            return CreatedAtAction(nameof(CreateAccountPost), new { id = newAcount.Id }, new
            {

                newAcount.Id,
                newAcount.Name,
                newAcount.Email,

            });

        }




    }
}
