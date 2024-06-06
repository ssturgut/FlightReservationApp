
using FlightApp.Application.Wrappers;
using FlightApp.Application.Interfaces;
using MediatR;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Interfaces.Factory;

namespace FlightApp.Application.Features.Commands.LoginCommands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ServiceResponse<LoginResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ITokenService _tokenService;
        private readonly IResponseFactory _responseFactory;

        public LoginCommandHandler(IClientRepository clientRepository, ITokenService tokenService, IResponseFactory responseFactory)
        {
            _clientRepository = clientRepository;
            _tokenService = tokenService;
            _responseFactory = responseFactory;
        }

        public async Task<ServiceResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByEmailAsync(request.EMail);

            if (client == null || !BCrypt.Net.BCrypt.Verify(request.Password, client.Password))
            {
                return _responseFactory.CreateErrorResponse<LoginResponse>("Invalid credentials");
            }

            var token = _tokenService.GenerateToken(client);

            return _responseFactory.CreateSuccessResponse(new LoginResponse
            {
                Token = token,
                IsSuccessful = true
            }, "Login Success");
        }
    }
}
