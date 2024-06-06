
using FlightApp.Domain.Entities;

namespace FlightApp.Application.Interfaces.Repository
{
    public interface IAviationService
    {
        Task<List<FlightInfo>> GetAllFlightsAsync();
    }
}