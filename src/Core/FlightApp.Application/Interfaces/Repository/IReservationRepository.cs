using System;
using FlightApp.Domain.Entities;

namespace FlightApp.Application.Interfaces.Repository
{
	public interface IReservationRepository
	{
        Task AddReservationAsync(Reservation reservation);
        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(string userId);
        Task<bool> DeleteReservationAsync(Guid reservationId);
    }
}

