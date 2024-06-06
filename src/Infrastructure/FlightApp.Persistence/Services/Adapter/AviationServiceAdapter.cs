using System;
using FlightApp.Application.Interfaces.Adapter;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Domain.Entities;

namespace FlightApp.Persistence.Services.Adapter
{
    public class AviationServiceAdapter : IAviationServiceAdapter
    {
        private readonly IAviationService _aviationService;

        public AviationServiceAdapter(IAviationService aviationService)
        {
            _aviationService = aviationService;
        }

        public async Task<List<FlightInfo>> GetAllFlightsAsync()
        {
            return await _aviationService.GetAllFlightsAsync();
        }
    }
}

