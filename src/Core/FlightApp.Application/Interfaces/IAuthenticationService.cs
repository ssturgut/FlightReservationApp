using FlightApp.Application.Dtos;
using FlightApp.Application.Wrappers;


namespace FlightApp.Application.Interfaces.Repository
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginViewDto model);
    }
}