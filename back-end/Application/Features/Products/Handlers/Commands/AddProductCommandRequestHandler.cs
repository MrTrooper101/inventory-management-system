using back_end.Application.Features.Products.Requests.Commands;
using back_end.Application.Interfaces;
using back_end.Domain.Entities;
using MediatR;

namespace back_end.Application.Features.Products.Handlers.Commands
{
    public class AddProductCommandRequestHandler : IRequestHandler<AddProductCommandRequest, bool>
    {
        private readonly IProductRepository _productRepository;

        public AddProductCommandRequestHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            var newProduct = new Product
            {
                Name = request.AddProduct.Name,
                Price = request.AddProduct.Price,
                CategoryId = request.AddProduct.CategoryId,
                Quantity = request.AddProduct.Quantity,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            var isAdded = await _productRepository.AddProductAsync(newProduct);
            return isAdded;
        }
    }
}
