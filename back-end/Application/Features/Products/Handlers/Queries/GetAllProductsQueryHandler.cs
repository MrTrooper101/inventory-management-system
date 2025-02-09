using back_end.Application.Features.Products.Requests.Queries;
using back_end.Application.Features.Products.Dtos;
using back_end.Application.Interfaces;
using MediatR;

namespace back_end.Application.Features.Products.Handlers.Queries
{

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsAsync();

            var productDtos = products.Select(c => new ProductDto
            {
                Id = c.Id,
                Name = c.Name,
                CategoryId = c.CategoryId,
                Price = c.Price,
                Quantity = c.Quantity,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return productDtos;
        }
    }
}
