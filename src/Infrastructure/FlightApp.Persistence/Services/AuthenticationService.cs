
using System.Text;
using FlightApp.Application.Dtos;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Wrappers;
using Newtonsoft.Json;

namespace FlightApp.Persistence.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> LoginAsync(LoginViewDto model)
        {
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseString);
                loginResponse.IsSuccessful = true;
                loginResponse.ErrorMessage = "Login Success";
                return loginResponse;
            }

            return new LoginResponse
            {
                IsSuccessful = false,
                ErrorMessage = "Invalid login attempt."
            };
        }
    }
}
