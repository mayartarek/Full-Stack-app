using Demo.Application.Command.Product.CreateProduct;
using Demo.Application.Command.Product.UpdateProduct;
using Demo.Application.Helper;
using Demo.Application.Model.Authuntication;
using Demo.Application.Query.Product.GetProductDetails;
using Demo.Application.Query.Product.GetProductList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("CreateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<bool>> Create([FromForm]CreateProdductRequest requestDtoVm)
        {
            var response = await mediator.Send(requestDtoVm);

            return Ok();
        }



        [HttpPut("UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> update([FromForm] UpdateProdductRequest requestDtoVm)
        {
            var response = await mediator.Send(requestDtoVm);

            return Ok();
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetProductDetailsVm), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Get([FromQuery] Guid Id)
        {
            var response = await mediator.Send(new GetProductDetailsQuery() { Id=Id});

            return Ok(response);
        }

        [HttpGet("GetALL")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PaginationList<GetProductListVm>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> GetAll([FromQuery] GetProductListQuery getProductListQuery)
        {
            var response = await mediator.Send(getProductListQuery);

            return Ok(response);
        }
    }
}
