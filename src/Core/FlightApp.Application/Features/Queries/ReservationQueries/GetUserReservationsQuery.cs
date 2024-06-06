using MediatR;
using FlightApp.Application.Dtos;
using FlightApp.Application.Wrappers;
using System.Collections.Generic;

namespace FlightApp.Application.Features.Queries
{
    public class GetUserReservationsQuery : IRequest<ReservationResponse>
    {
        public string UserId { get; set; }
    }
}
