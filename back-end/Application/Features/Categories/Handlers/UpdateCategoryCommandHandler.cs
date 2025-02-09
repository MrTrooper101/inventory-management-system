using back_end.Application.Features.Categories.Commands;
using back_end.Application.Interfaces;
using back_end.Domain.Entities;
using MediatR;

namespace back_end.Application.Features.Categories.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category
            {
                Id = request.UpdateCategoryRequest.Id,
                Name = request.UpdateCategoryRequest.Name,
                IsActive = true,
                UpdatedAt = DateTime.UtcNow
            };

            var isAdded = await _categoryRepository.UpdateCategoryAsync(newCategory);
            return isAdded;
        }
    }
}
