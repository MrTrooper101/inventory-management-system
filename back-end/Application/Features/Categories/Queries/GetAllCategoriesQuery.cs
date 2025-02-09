using MediatR;
using back_end.Application.Features.Categories.Dtos;

namespace back_end.Application.Features.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}
