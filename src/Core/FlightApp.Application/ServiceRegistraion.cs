using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FlightApp.Application
{
	public static class ServiceRegistraion
	{
		public static void AddApplicationRegistration(this IServiceCollection services)
		{
			var assm = Assembly.GetExecutingAssembly();

			services.AddAutoMapper(assm);
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assm));
		}
	}
}

