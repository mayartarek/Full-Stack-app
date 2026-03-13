using Demo.Application.Constract.Interface;
using System.Security.Claims;

namespace Demo.Api.Service
{
    public class LoggedService : ILoggedInerface
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoggedService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get
            {
                return _contextAccessor.HttpContext?.User?.FindFirstValue("uid");
            }
        }
    }
}
