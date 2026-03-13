using Demo.Application.Helper;
using Demo.Application.Query.Category.GetCategoruLookUp;
using Demo.Application.Query.Product.GetProductList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly IMediator mediator;

        public LookupController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("GetALLCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<GetCategoruLookUpVm>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult<List<GetCategoruLookUpVm>>> GetAll()
        {
            var response = await mediator.Send(new GetCategoruLookUpQuery());

            return Ok(response);
        }
    }
}
