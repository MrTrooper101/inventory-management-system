using back_end.Application.Features.Categories.Commands;
using back_end.Application.Interfaces;
using back_end.Domain.Entities;
using MediatR;

namespace back_end.Application.Features.Categories.Handlers
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        public AddCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category
            {
                Name = request.AddCategoryRequest.Name,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            var isAdded = await _categoryRepository.AddCategoryAsync(newCategory);
            return isAdded;
        }
    }
}
