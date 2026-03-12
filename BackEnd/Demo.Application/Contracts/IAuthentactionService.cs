using Demo.Application.Model.Authuntication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Contracts
{
    public interface IAuthentactionService
    {
        Task<AuthenticationResponseDto> Authentication(AuthenticationRequestDtoVm authenticationDto);
        Task<bool> LogoutAsync();
    }
}
