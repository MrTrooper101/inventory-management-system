using back_end.Application.Features.Categories.Dtos;
using MediatR;

namespace back_end.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public UpdateCategoryDto UpdateCategoryRequest { get; set; }
    }
}
