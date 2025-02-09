using back_end.Application.Features.Categories.Dtos;
using MediatR;

namespace back_end.Application.Features.Categories.Commands
{
    public class AddCategoryCommand : IRequest<bool>
    {
        public AddCategoryDto AddCategoryRequest { get; set; }
    }
}
