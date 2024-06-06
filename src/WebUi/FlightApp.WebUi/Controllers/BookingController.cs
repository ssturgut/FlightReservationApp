
using System.Security.Claims;
using FlightApp.Application.Dtos;
using FlightApp.Application.Features.Commands.ReservationCommand;
using FlightApp.Application.Settings;
using FlightApp.Application.ViewModels;
using FlightApp.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FlightApp.WebUi.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public BookingController(IMediator mediator, IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _mediator = mediator;
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Confirmation");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlights()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_apiSettings.BaseUrl}/flight");
            response.EnsureSuccessStatusCode();

            var serviceResponse = await response.Content.ReadFromJsonAsync<ServiceResponse<List<FlightViewDto>>>();

            if (serviceResponse != null && serviceResponse.Success)
            {
                return Json(serviceResponse.Data);
            }

            return Json(new List<FlightViewDto>());
        }

        [HttpPost]
        public async Task<IActionResult> SearchFlights(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync($"{_apiSettings.BaseUrl}/flight");
                response.EnsureSuccessStatusCode();

                var serviceResponse = await response.Content.ReadFromJsonAsync<ServiceResponse<List<FlightViewDto>>>();

                if (serviceResponse != null && serviceResponse.Success)
                {
                    var matchingFlights = serviceResponse.Data
                        .Where(f => f.DepartureAirport == model.FlyingFrom && f.ArrivalAirport == model.FlyingTo)
                        .ToList();

                    if (matchingFlights.Count > 0)
                    {
                        TempData["Flights"] = JsonConvert.SerializeObject(matchingFlights);
                        return RedirectToAction("ShowFlights");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "No flights found. Please check other flights.";
                        return View("Index", model);
                    }
                }

                ViewBag.ErrorMessage = "Error fetching flights. Please try again later.";
                return View("Index", model);
            }

            return View("Index", model);
        }

        public IActionResult ShowFlights()
        {
            var flights = JsonConvert.DeserializeObject<List<FlightViewDto>>(TempData["Flights"]?.ToString());
            return View(flights);
        }

        [HttpPost]
        public async Task<IActionResult> BookFlight(string FlightIata, string DepartureAirport, string ArrivalAirport, int Adults, int Children, string TripType, DateTime Departing, DateTime? Returning)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            int NumberOfSeats = (Adults + Children) < 1 ? 1 : (Adults + Children);

            var outboundReservation = new CreateReservationCommand
            {
                UserId = userId,
                FlightIata = FlightIata,
                DepartureAirport = DepartureAirport,
                ArrivalAirport = ArrivalAirport,
                BookingDate = Departing,
                NumberOfSeats = NumberOfSeats
            };

            var result = await _mediator.Send(outboundReservation);

            if (result.Success && TripType == "Roundtrip" && Returning.HasValue)
            {
                var returnReservation = new CreateReservationCommand
                {
                    UserId = userId,
                    FlightIata = FlightIata,
                    DepartureAirport = ArrivalAirport,
                    ArrivalAirport = DepartureAirport,
                    BookingDate = Returning.Value,
                    NumberOfSeats = NumberOfSeats
                };

                result = await _mediator.Send(returnReservation);
            }

            if (result.Success)
            {
                return RedirectToAction("Confirmation");
            }
            else
            {
                ModelState.AddModelError("", "An error occurred while booking the flight.");
                return View("Index");
            }
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
