using System;
using FlightApp.Domain.Entities;

namespace FlightApp.Application.Interfaces
{
	public interface ITokenService
	{
		string GenerateToken(Client client);
	}
}

