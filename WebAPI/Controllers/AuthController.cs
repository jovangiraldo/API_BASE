using API.Application.DTOs;
using API.Application.Interfaces;
using API.Domain.Entities;
using API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly ICreateAccount<CreateAccount> _createAccount;
        private readonly IRepository<CreateAccount> _repository;

        public AuthController(IJwtService jwtService,ICreateAccount<CreateAccount> createAccount, IRepository<CreateAccount> repository)
        {
            _jwtService = jwtService;
            _createAccount = createAccount;
            _repository = repository;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var account = _createAccount.GetByCorreo(loginDTO.Email);

            if (account== null)
            {
                return Unauthorized(new { message = "Correo o Contrasena incorrectas" });

            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, account.Password);

            if (!isValidPassword)
            {
                return Unauthorized(new { message = "Correo o contraseña incorrectos." });
            }

            var token = _jwtService.GenerateJwtToken(account.Email, account.Role.ToString());

            return Ok(new { token, role = account.Role.ToString(), email = account.Email, userId = account.Id, });
        }
    }
}
