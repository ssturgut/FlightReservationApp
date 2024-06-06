using System;
using AutoMapper;
using FlightApp.Application.Dtos;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Wrappers;
using MediatR;

namespace FlightApp.Application.Features.Queries.GetAllProducts
{
	public class GetAllProductsQuery : IRequest<ServiceResponse<List<ProductViewDto>>>
	{
        
    }
}

