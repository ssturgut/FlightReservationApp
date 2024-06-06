
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FlightApp.Application.Features.Queries.GetAllProducts;
using FlightApp.Application.Features.Commands;
using FlightApp.Application.Features.Queries.GetProductById;


namespace FlightApp.WebApi.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllProductsQuery();
            var result = await mediator.Send(query);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetProductByIdQuery() {Id = id};

            return Ok(await mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
