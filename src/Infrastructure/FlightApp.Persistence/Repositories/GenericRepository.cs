
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Domain.Common;
using FlightApp.Persistence.Context;
using MongoDB.Driver;

namespace FlightApp.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepositoryAsync<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(MongoDbContext dbContext)
        {
            _collection = dbContext.GetCollection<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(e => e.Id == id);
        }
    }
}
