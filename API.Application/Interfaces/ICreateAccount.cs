using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Interfaces
{
    public interface ICreateAccount<T> where T : class
    {
        T GetByCorreo(string correo);
    }
}
