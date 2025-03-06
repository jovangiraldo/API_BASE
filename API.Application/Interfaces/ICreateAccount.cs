using API.Application.DTOs;
using API.Domain.Entities;

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

        IEnumerable<UserDTO> GetDataNameId();
    }
}
