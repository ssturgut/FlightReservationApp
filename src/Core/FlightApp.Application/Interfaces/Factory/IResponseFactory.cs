using System;
using FlightApp.Application.Wrappers;

namespace FlightApp.Application.Interfaces.Factory
{
    public interface IResponseFactory
    {
        ServiceResponse<T> CreateSuccessResponse<T>(T data, string message);
        ServiceResponse<T> CreateErrorResponse<T>(string message);
    }
}

