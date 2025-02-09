using back_end.Application.Features.Categories.Commands;
using back_end.Application.Interfaces;
using MediatR;

namespace back_end.Application.Features.Categories.Handlers
{

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await _categoryRepository.DeleteCategoryAsync(request.DeleteCategoryRequest.Id);
            return isDeleted;
        }
    }
}
