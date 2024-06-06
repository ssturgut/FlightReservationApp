using System;
using Microsoft.EntityFrameworkCore;
using FlightApp.Domain.Entities;

namespace FlightApp.Persistence.Context
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Product>().HasData(
				new Product() { Id=Guid.NewGuid(), Name="Pen", Value=10, Quantity=100, CreateDate= DateTime.Now},
                new Product() { Id = Guid.NewGuid(), Name = "Paper", Value = 101, Quantity = 1200, CreateDate = DateTime.Now },
                new Product() { Id = Guid.NewGuid(), Name = "Book", Value = 1, Quantity = 1020, CreateDate = DateTime.Now },
                new Product() { Id = Guid.NewGuid(), Name = "Eraser", Value = 2, Quantity = 1100, CreateDate = DateTime.Now }
                );
            base.OnModelCreating(modelBuilder);
        }

	}
}

