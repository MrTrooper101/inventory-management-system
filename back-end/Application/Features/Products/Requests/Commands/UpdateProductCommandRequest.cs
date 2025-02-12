using back_end.Application.Features.Products.Dtos;
using MediatR;

namespace back_end.Application.Features.Products.Requests.Commands
{
    public class UpdateProductCommandRequest : IRequest<bool>
    {
        public ProductDto UpdateProduct { get; set; }
    }
}
