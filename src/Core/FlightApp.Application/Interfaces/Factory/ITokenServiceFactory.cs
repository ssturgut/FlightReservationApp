using System;
namespace FlightApp.Application.Interfaces.Factory
{
	public interface ITokenServiceFactory
	{
        ITokenService CreateTokenService();
    }
}

