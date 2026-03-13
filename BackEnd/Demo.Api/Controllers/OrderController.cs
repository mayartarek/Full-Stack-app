using Demo.Application.Command.Order.CreateOrderCommand;
using Demo.Application.Command.Product.CreateProduct;
using Demo.Application.Query.Order.GetOrdersList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("CreateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Create([FromBody] CreateOrderCommand requestDtoVm)
        {
            var response = await mediator.Send(requestDtoVm);

            return Ok();
        }
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<GetOrdersListvm>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Get()
        {
            var response = await mediator.Send(new GetOrdersListQuery());

            return Ok(response);
        }
    }
}
