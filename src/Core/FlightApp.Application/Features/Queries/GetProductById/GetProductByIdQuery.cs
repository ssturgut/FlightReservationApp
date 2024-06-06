using System;
using FlightApp.Application.Dtos;
using FlightApp.Application.Wrappers;
using MediatR;

namespace FlightApp.Application.Features.Queries.GetProductById
{
	public class GetProductByIdQuery : IRequest<ServiceResponse<GetProductByIdViewModel>>
	{
		public Guid Id { get; set; }
		public GetProductByIdQuery()
		{
		}
	}
}

