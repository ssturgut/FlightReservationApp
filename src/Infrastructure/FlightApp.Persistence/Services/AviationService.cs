
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Settings;
using FlightApp.Application.Wrappers;
using FlightApp.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace FlightApp.Persistence.Services
{
    public class AviationService: IAviationService
    {
        private readonly HttpClient _httpClient;
        private readonly AviationApiSettings _settings;

        public AviationService(HttpClient httpClient, IOptions<AviationApiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<List<FlightInfo>> GetAllFlightsAsync()
        {
            var url = $"{_settings.BaseUrl}v1/flights?access_key={_settings.ApiKey}";
            var response = await _httpClient.GetFromJsonAsync<AviationApiResponse>(url);

            if (response?.Data == null)
            {
                return new List<FlightInfo>();
            }

            return response.Data;
        }
    }
}