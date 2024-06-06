using FlightApp.Application.Wrappers;
using MediatR;

namespace FlightApp.Application.Features.Commands.ReservationCommand
{

    public class CreateReservationCommand : IRequest<ReservationResponse>
    {
        public string UserId { get; set; }
        public string FlightIata { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfSeats { get; set; }
    }


}

