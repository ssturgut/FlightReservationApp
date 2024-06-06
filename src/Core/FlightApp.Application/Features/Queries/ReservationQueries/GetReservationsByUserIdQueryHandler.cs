
using FlightApp.Application.Dtos;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Wrappers; 
using MediatR;

namespace FlightApp.Application.Features.Queries.GetUserReservations
{
    public class GetUserReservationsQuery : IRequest<ReservationResponse>
    {
        public string UserId { get; set; }
    }

    public class GetUserReservationsQueryHandler : IRequestHandler<GetUserReservationsQuery, ReservationResponse>
    {
        private readonly IReservationRepository _reservationRepository;

        public GetUserReservationsQueryHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ReservationResponse> Handle(GetUserReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetReservationsByUserIdAsync(request.UserId);
            var reservationDtos = reservations.Select(r => new ReservationViewDto
            {
                Id = r.Id,
                UserId = r.UserId,
                FlightIata = r.FlightIata,
                DepartureAirport = r.DepartureAirport,
                ArrivalAirport = r.ArrivalAirport,
                BookingDate = r.BookingDate,
                NumberOfSeats = r.NumberOfSeats
            }).ToList();

            return new ReservationResponse(reservationDtos);
        }
    }
}
