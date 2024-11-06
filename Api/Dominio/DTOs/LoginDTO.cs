using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Dominio.DTOs
{
    public class LoginDTO
    {
        public String Email { get; set; } = default!;
        public String Senha { get; set; } = default!;
    }
}