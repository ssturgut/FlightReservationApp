
using AutoMapper;
using FlightApp.Application.Dtos;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Wrappers;
using MediatR;


namespace FlightApp.Application.Features.Queries.GetAllFlights
{
    public class GetAllFlightsQueryHandler : IRequestHandler<GetAllFlightsQuery, ServiceResponse<List<FlightViewDto>>>
    {
        private readonly IAviationService _aviationService;
        private readonly IMapper _mapper;

        public GetAllFlightsQueryHandler(IAviationService aviationService, IMapper mapper)
        {
            _aviationService = aviationService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<FlightViewDto>>> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
        {
            var flightInfos = await _aviationService.GetAllFlightsAsync();

            if (flightInfos == null || flightInfos.Count == 0)
            {
                return new ServiceResponse<List<FlightViewDto>>
                {
                    Data = new List<FlightViewDto>(),
                    Success = false,
                    Message = "No flights found."
                };
            }

            var flightViewDtos = _mapper.Map<List<FlightViewDto>>(flightInfos);

            return new ServiceResponse<List<FlightViewDto>>
            {
                Data = flightViewDtos,
                Success = true,
                Message = "Flights retrieved successfully."
            };
        }
    }
}