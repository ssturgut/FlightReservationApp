//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using FlightApp.Application.Dtos;
//using FlightApp.Application.Features.Queries.MatchFlights;
//using MediatR;

//namespace FlightApp.Application.Features.Queries.MatchFlights
//{
//    public class MatchFlightsQueryHandler : IRequestHandler<MatchFlightsQuery, List<FlightViewDto>>
//    {
//        public Task<List<FlightViewDto>> Handle(MatchFlightsQuery request, CancellationToken cancellationToken)
//        {
//            var matchingFlights = request.Flights
//                .Where(f => f.DepartureAirport == request.FlyingFrom && f.ArrivalAirport == request.FlyingTo && f.FlightDate == request.DepartureDate)
//                .ToList();

//            if (request.TripType == "Roundtrip" && request.ReturnDate.HasValue)
//            {
//                var returnFlights = request.Flights
//                    .Where(f => f.DepartureAirport == request.FlyingTo && f.ArrivalAirport == request.FlyingFrom && f.FlightDate == request.ReturnDate.Value)
//                    .ToList();

//                matchingFlights.AddRange(returnFlights);
//            }

//            return Task.FromResult(matchingFlights);
//        }
//    }
//}

