using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FlightApp.Application.Features.Commands.LoginCommands;
using FlightApp.Application.Wrappers;

namespace FlightApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Success || !response.Data.IsSuccessful)
            {
                return Unauthorized(new { response.Data.ErrorMessage });
            }
            return Ok(new { response.Data.Token });
        }
    }
}
