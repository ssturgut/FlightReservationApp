using System;
using AutoMapper;
using FlightApp.Application.Dtos;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Wrappers;
using MediatR;

namespace FlightApp.Application.Features.Queries.GetAllFlights
{
	public class GetAllFlightsQuery : IRequest<ServiceResponse<List<FlightViewDto>>>
	{
		public GetAllFlightsQuery()
		{
		}
	}
}

