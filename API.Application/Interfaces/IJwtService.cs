using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(string username,string role);
    }
}
