using back_end.Application.Features.Products.Requests.Commands;
using back_end.Application.Interfaces;
using back_end.Domain.Entities;
using MediatR;

namespace back_end.Application.Features.Products.Handlers.Commands
{
    public class UpdateProductCommandRequestHandler : IRequestHandler<UpdateProductCommandRequest, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandRequestHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var newProduct = new Product
            {
                Id = request.UpdateProduct.Id,
                Name = request.UpdateProduct.Name,
                Price = request.UpdateProduct.Price,
                CategoryId = request.UpdateProduct.CategoryId,
                Quantity = request.UpdateProduct.Quantity,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            var isUpdated = await _productRepository.UpdateProductAsync(newProduct);
            return isUpdated;
        }
    }

}
