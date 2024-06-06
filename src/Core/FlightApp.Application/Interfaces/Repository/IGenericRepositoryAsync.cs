using System;
using FlightApp.Domain.Common;
using FlightApp.Domain.Entities;

namespace FlightApp.Application.Interfaces.Repository
{
	public interface IGenericRepositoryAsync<T> where T : BaseEntity
	{
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task<T> AddAsync(T entity);

    }
}

