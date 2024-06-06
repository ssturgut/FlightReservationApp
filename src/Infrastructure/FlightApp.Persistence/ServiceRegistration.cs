using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Interfaces;
using FlightApp.Persistence.Context;
using FlightApp.Persistence.Repositories;
using FlightApp.Application.Settings;
using FlightApp.Persistence.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FlightApp.Application.Interfaces.Factory;
using FlightApp.Persistence.Services.Factory;

namespace FlightApp.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind MongoDB settings
            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDBSettings"));

            // Register MongoDB context
            services.AddSingleton<MongoDbContext>();

            // Register repositories and services
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAviationService, AviationService>();
            services.AddTransient<IReservationRepository, ReservationRepository>();

        }
    }
}