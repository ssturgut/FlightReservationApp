
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Domain.Entities;
using MongoDB.Driver;

namespace FlightApp.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _clients;

        public ClientRepository(IMongoDatabase database)
        {
            _clients = database.GetCollection<Client>("Client");
        }

        public async Task<Client> GetClientByEmailAsync(string email)
        {
            return await _clients.Find(c => c.EMail == email).FirstOrDefaultAsync();
        }

    }
}
