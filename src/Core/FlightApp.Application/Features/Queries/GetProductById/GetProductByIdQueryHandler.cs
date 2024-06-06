
using AutoMapper;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Wrappers;
using MediatR;

namespace FlightApp.Application.Features.Queries.GetProductById
{
	public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ServiceResponse<GetProductByIdViewModel>>
	{
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
		{
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<GetProductByIdViewModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetByIdAsync(request.Id);
            var viewModel = mapper.Map<GetProductByIdViewModel>(products);
            return new ServiceResponse<GetProductByIdViewModel>(viewModel);
        }
    }
}

