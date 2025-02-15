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
    }
}
