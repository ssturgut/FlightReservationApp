using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Wrappers;
using FlightApp.Domain.Entities;
using MediatR;

namespace FlightApp.Application.Features.Commands.ReservationCommand
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ReservationResponse>
    {
        private readonly IReservationRepository _reservationRepository;
     

        public CreateReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
           
        }

        public async Task<ReservationResponse> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var reservation = new Reservation
                {
                    UserId = request.UserId,
                    FlightIata = request.FlightIata,
                    DepartureAirport = request.DepartureAirport,
                    ArrivalAirport = request.ArrivalAirport,
                    BookingDate = request.BookingDate,
                    NumberOfSeats = request.NumberOfSeats
                };

                await _reservationRepository.AddReservationAsync(reservation);
               
                return new ReservationResponse { Success = true, Message = "Reservation created successfully" };
            }
            catch (Exception ex)
            {
               
                return new ReservationResponse { Success = false, Message = $"Error creating reservation: {ex.Message}" };
            }
        }
    }
}