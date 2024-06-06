using System;
using FlightApp.Domain.Entities;

namespace FlightApp.Application.Interfaces.Repository
{
	public interface IClientRepository
	{
		Task<Client> GetClientByEmailAsync(string email);
	}
}

