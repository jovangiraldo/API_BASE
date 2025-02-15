using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Domain.Interfaces;
using API.Infrastructure.Repository;
using API.Domain.Entities;
using API.Application.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepository<CreateAccount> _repository;

        public AccountController(IRepository<CreateAccount> repository)
        {
            _repository = repository;
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


        [HttpGet]
        public ActionResult GetByIdAccount(int id) {

            try
            {
                var obj = _repository.GetById(id);

                if (obj == null)
                {
                    NotFound(new { message = $"No se encontró un usuario con ID {id}." });
                }
                return Ok(obj);

            }catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno en el servidor.", error = ex.Message });
            }      

        }



        //[HttpPost]
        //public ActionResult Postdata([FromBody] CreateAccountDTO createAccount) {

        //    if (createAccount != null && createAccount.is)
        //    {
                
        //    }

        //}

        


    }
}
