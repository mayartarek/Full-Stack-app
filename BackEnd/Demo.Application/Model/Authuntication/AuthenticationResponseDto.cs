using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Model.Authuntication
{
    public class AuthenticationResponseDto
    {
        public string? Token { get; set; }
        public DateTime? ExpiresOn { get; set; }

    }
}
