using MediatR;

namespace back_end.Application.Features.Products.Requests.Commands
{
    public class DeleteProductCommandRequest : IRequest<bool>
    {
        public int ProductId { get; set; }

        public DeleteProductCommandRequest(int productId)
        {
            ProductId = productId;
        }
    }
}
