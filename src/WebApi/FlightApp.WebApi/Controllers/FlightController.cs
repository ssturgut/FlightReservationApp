using AutoMapper;
using FlightApp.Application.Dtos;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IAviationService _aviationService;
        private readonly IMapper _mapper;

        public FlightController(IAviationService aviationService, IMapper mapper)
        {
            _aviationService = aviationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlights()
        {
            var flights = await _aviationService.GetAllFlightsAsync();
            var flightDtos = _mapper.Map<List<FlightViewDto>>(flights);

            return Ok(new ServiceResponse<List<FlightViewDto>>(flightDtos));
        }
    }
}
