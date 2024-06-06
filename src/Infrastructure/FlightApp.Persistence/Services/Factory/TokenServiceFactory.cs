using System;
using FlightApp.Application.Common.Services;
using FlightApp.Application.Interfaces;
using FlightApp.Application.Interfaces.Factory;
using Microsoft.Extensions.Configuration;

namespace FlightApp.Persistence.Services.Factory
{
    public class TokenServiceFactory : ITokenServiceFactory
    {
        private readonly IConfiguration _configuration;

        public TokenServiceFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ITokenService CreateTokenService()
        {
            return new TokenService(_configuration);
        }
    }

}

