using MediatR;
using FlightApp.Application.Wrappers;


namespace FlightApp.Application.Features.Commands.ReservationCommands
{
    public class CancelReservationCommand : IRequest<ReservationResponse>
    {
        public Guid ReservationId { get; set; }
    }
}
