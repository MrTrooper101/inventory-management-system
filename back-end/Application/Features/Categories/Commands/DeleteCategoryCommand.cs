using back_end.Application.Features.Categories.Dtos;
using MediatR;

namespace back_end.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public DeleteCategoryDto DeleteCategoryRequest { get; set; }
    }
}
