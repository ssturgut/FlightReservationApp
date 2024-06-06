using System;
using AutoMapper;
using FlightApp.Application.Dtos;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Wrappers;
using MediatR;

namespace FlightApp.Application.Features.Queries.GetAllProducts
{
    public class GetAllProductsQuryHandler : IRequestHandler<GetAllProductsQuery, ServiceResponse<List<ProductViewDto>>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetAllProductsQuryHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<List<ProductViewDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllAsync();

            var viewModel = mapper.Map<List<ProductViewDto>>(products);

            return new ServiceResponse<List<ProductViewDto>>(viewModel);
        }
    }
}

