using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Model.Authuntication
{
    public class AuthenticationRequestDtoVm
    {
        public string? Email { get; set; }
        public string Password { get; set; } = null!;   
    }
}
