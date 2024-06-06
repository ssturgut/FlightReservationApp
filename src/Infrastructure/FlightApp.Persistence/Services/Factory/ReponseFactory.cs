using FlightApp.Application.Interfaces;
using FlightApp.Application.Interfaces.Factory;
using FlightApp.Application.Wrappers;

namespace FlightApp.Application.Services
{
    public class ResponseFactory : IResponseFactory
    {
        public ServiceResponse<T> CreateSuccessResponse<T>(T data, string message)
        {
            return new ServiceResponse<T>
            {
                Data = data,
                Success = true,
                Message = message
            };
        }

        public ServiceResponse<T> CreateErrorResponse<T>(string message)
        {
            return new ServiceResponse<T>
            {
                Data = default(T),
                Success = false,
                Message = message
            };
        }
    }
}
