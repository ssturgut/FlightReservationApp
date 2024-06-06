
using FlightApp.Application.Wrappers;
using MediatR;

namespace FlightApp.Application.Features.Commands.LoginCommands
{
	public class LoginCommand : IRequest<ServiceResponse<LoginResponse>>
	{
		public string EMail { get; set; }
		public string Password { get; set; }
	}
}

