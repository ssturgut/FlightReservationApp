
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Wrappers;
using MediatR;

namespace FlightApp.Application.Features.Commands.ReservationCommand
{
    public class CancelReservationCommand : IRequest<ReservationResponse>
    {
        public Guid ReservationId { get; set; }
    }

    public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand, ReservationResponse>
    {
        private readonly IReservationRepository _reservationRepository;

        public CancelReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ReservationResponse> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var success = await _reservationRepository.DeleteReservationAsync(request.ReservationId);
            if (success)
            {
                return new ReservationResponse("Reservation successfully canceled.");
            }
            return new ReservationResponse("Error occurred while canceling the reservation.");
        }
    }
}
