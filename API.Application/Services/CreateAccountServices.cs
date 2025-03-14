using API.Application.DTOs;
using API.Domain.Entities;
using API.Domain.Interfaces;
using API.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.Services
{
    public class CreateAccountServices<T> : ICreateAccount<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public CreateAccountServices(ApplicationDbContext context) { 

            _context = context;
        }

        public T GetByCorreo(string correo)
        {
            return _context.Set<T>().FirstOrDefault(x => EF.Property<string>(x, "Email") == correo);
        }

        public IEnumerable<UserDTO> GetDataNameId()
        {
            try
            {
                var data = _context.CreateAccounts
                                   .Select(obj => new UserDTO { Id = obj.Id, Name = obj.Name })
                                   .ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos de la base de datos", ex);
            }
        }

    }
}
