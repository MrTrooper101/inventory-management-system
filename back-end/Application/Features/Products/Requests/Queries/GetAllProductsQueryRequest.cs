using back_end.Application.Features.Products.Dtos;
using MediatR;

namespace back_end.Application.Features.Products.Requests.Queries
{
    public class GetAllProductsQueryRequest:IRequest<List<ProductDto>>
    {
    }
}
