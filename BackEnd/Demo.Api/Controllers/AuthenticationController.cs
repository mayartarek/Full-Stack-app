

using Demo.Application.Model.Authuntication;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly Demo.Application.Contracts.IAuthentactionService _service;

        public AuthenticationController(Demo.Application.Contracts.IAuthentactionService service)
        {
            _service = service;
        }
        /// <summary>
        /// Authenticates a user based on the provided credentials and returns authentication details if successful.
        /// </summary>
        /// <remarks>This method is typically used to initiate a login process. The response includes
        /// authentication tokens and user information if authentication succeeds. No content is returned if
        /// authentication fails, and a 404 status is returned if the user does not exist.</remarks>
        /// <param name="requestDtoVm">The authentication request data containing user credentials and any additional information required for
        /// authentication. Cannot be null.</param>
        /// <returns>An <see cref="ActionResult{AuthenticationResponseDto}"/> containing authentication information if the
        /// credentials are valid; returns 404 if the user is not found, or 204 if authentication fails.</returns>
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AuthenticationResponseDto), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AuthenticationResponseDto>> Authentication(AuthenticationRequestDtoVm requestDtoVm)
        {
            var response = await _service.Authentication(requestDtoVm);
           
            return Ok(response);
        }

    }
}
