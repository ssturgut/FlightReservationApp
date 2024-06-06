
using AutoMapper;
using FlightApp.Application.Interfaces.Repository;
using FlightApp.Application.Wrappers;
using MediatR;

namespace FlightApp.Application.Features.Commands
{
	public class CreateProductCommand : IRequest<ServiceResponse<Guid>>
	{
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<Guid>>
        {
            private readonly IProductRepository productRepository;
            private readonly IMapper mapper;

            public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                this.productRepository = productRepository;
                this.mapper = mapper;
            }

            public async Task<ServiceResponse<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var product = mapper.Map<Domain.Entities.Product>(request);
                await productRepository.AddAsync(product);
                return new ServiceResponse<Guid>(product.Id);
            }
        }
    }
}

