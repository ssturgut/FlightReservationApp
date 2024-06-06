using FlightApp.Application.Features.Queries.GetAllFlights;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightApp.WebUi.Controllers
{
    public class FlightController : Controller
    {
        private readonly IMediator _mediator;

        public FlightController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var query = new GetAllFlightsQuery();
            var result = await _mediator.Send(query);

            if (result.Success)
            {
                return View(result.Data);  
            }

            return View(new List<FlightViewDto>());  
        }
    }
}

