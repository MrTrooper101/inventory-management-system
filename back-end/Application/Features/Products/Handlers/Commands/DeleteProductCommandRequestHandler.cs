using back_end.Application.Features.Products.Requests.Commands;
using back_end.Application.Interfaces;
using back_end.Domain.Entities;
using MediatR;

namespace back_end.Application.Features.Products.Handlers.Commands
{
    public class DeleteProductCommandRequestHandler : IRequestHandler<DeleteProductCommandRequest, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandRequestHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var isDeleted = await _productRepository.DeleteProductAsync(request.ProductId);
            return isDeleted;
        }
    }
}
