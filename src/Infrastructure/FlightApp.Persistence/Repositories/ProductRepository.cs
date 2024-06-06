using System;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Domain.Entities;
using FlightApp.Persistence.Context;

namespace FlightApp.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MongoDbContext dbContext) : base(dbContext)
        {
        }
    }
}

