using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using FlightApp.Domain.Entities;
using FlightApp.Application.Settings;
using FlightApp.Domain.Common;

namespace FlightApp.Persistence.Context
{
	public class MongoDbContext
	{
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDBSettings> settings)
		{
            if (settings == null || string.IsNullOrEmpty(settings.Value.ConnectionString) || string.IsNullOrEmpty(settings.Value.DatabaseName))
            {
                throw new ArgumentNullException("MongoDB settings are not properly configured.");
            }

            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);

            InitializeDatabase();
        }

        public IMongoCollection<T> GetCollection<T>() where T : BaseEntity
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }


        public IMongoCollection<Client> Client => _database.GetCollection<Client>("Client");

        public void InitializeDatabase()
        {
            if (!_database.ListCollectionNames().ToList().Contains("Product"))
            {
                _database.CreateCollection("Product");
                SeedProductData();
            }

            if (!_database.ListCollectionNames().ToList().Contains("Client"))
            {
                _database.CreateCollection("Client");
                SeedClientData();
            }

            if (!_database.ListCollectionNames().ToList().Contains("Reservation"))
            {
                _database.CreateCollection("Reservation");
                SeedReservationData();
            }
        }


        public void SeedData()
        {
            SeedProductData();
            SeedClientData();
            SeedReservationData();
        }

        private void SeedProductData()
        {
            var products = _database.GetCollection<Product>("Product");
            if (products.CountDocuments(FilterDefinition<Product>.Empty) == 0)
            {
                products.InsertMany(new[]
                {
                    new Product { Id = Guid.NewGuid(), Name = "Pen", Value = 10, Quantity = 100, CreateDate = DateTime.Now },
                    new Product { Id = Guid.NewGuid(), Name = "Paper", Value = 101, Quantity = 1200, CreateDate = DateTime.Now },
                    new Product { Id = Guid.NewGuid(), Name = "Book", Value = 1, Quantity = 1020, CreateDate = DateTime.Now },
                    new Product { Id = Guid.NewGuid(), Name = "Eraser", Value = 2, Quantity = 1100, CreateDate = DateTime.Now }
                });
            }
        }

        private void SeedClientData()
        {
            var clients = _database.GetCollection<Client>("Client");
            if (clients.CountDocuments(FilterDefinition<Client>.Empty) == 0)
            {
                clients.InsertOne(new Client
                {
                    Id = Guid.NewGuid(),
                    Name = "God",
                    Surname = "God",
                    PassportNumber = "A12345678",
                    EMail = "god@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password")
                });
            }
        }

        private void SeedReservationData()
        {
            var products = _database.GetCollection<Reservation>("Reservation");
            if (products.CountDocuments(FilterDefinition<Reservation>.Empty) == 0)
            {
                products.InsertMany(new[]
                {
                    new Reservation { Id = Guid.NewGuid(), ArrivalAirport="Ankara" , BookingDate = DateTime.Now, DepartureAirport="İstanbul" , FlightIata= "TR365", UserId="OTS6s/rOOk+qR1nD1YHUqw", NumberOfSeats=3},
                    new Reservation { Id = Guid.NewGuid(), ArrivalAirport="Ankara" , BookingDate = DateTime.Now, DepartureAirport="İstanbul" , FlightIata= "TR365", UserId="OTS6s/rOOk+qR1nD1YHUqw", NumberOfSeats=3},
                    new Reservation { Id = Guid.NewGuid(), ArrivalAirport="Ankara" , BookingDate = DateTime.Now, DepartureAirport="İstanbul" , FlightIata= "TR365", UserId="OTS6s/rOOk+qR1nD1YHUqw", NumberOfSeats=3},
                    new Reservation { Id = Guid.NewGuid(), ArrivalAirport="Ankara" , BookingDate = DateTime.Now, DepartureAirport="İstanbul" , FlightIata= "TR365", UserId="OTS6s/rOOk+qR1nD1YHUqw", NumberOfSeats=3}
                });
            }
        }
    }
}