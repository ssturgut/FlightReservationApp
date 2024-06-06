using FlightApp.Application.Interfaces.Repository;
using FlightApp.Domain.Entities;
using FlightApp.Persistence.Context;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace FlightApp.Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IMongoCollection<Reservation> _reservations;
        private readonly ILogger<ReservationRepository> _logger;

        public ReservationRepository(IMongoDatabase database, ILogger<ReservationRepository> logger)
        {
            _reservations = database.GetCollection<Reservation>("Reservation");
            _logger = logger;
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            try
            {
                await _reservations.InsertOneAsync(reservation);
                _logger.LogInformation("Reservation added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding reservation: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(string userId)
        {
            return await _reservations.Find(r => r.UserId == userId).ToListAsync();
        }

        public async Task<bool> DeleteReservationAsync(Guid reservationId)
        {
            var result = await _reservations.DeleteOneAsync(r => r.Id == reservationId);
            return result.DeletedCount > 0;
        }
    }
}
