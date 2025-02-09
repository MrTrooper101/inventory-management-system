using back_end.Application.Interfaces;
using back_end.Domain.Entities;
using back_end.Infastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace back_end.Infastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Where(c => c.IsActive).ToListAsync();
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            var existingCategory = await _context.Categories
                                        .FirstOrDefaultAsync(c => c.Name == category.Name && c.IsActive);

            if (existingCategory != null)
            {
                return false;
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _context.Categories
                                      .FirstOrDefaultAsync(c => c.Id == category.Id && c.IsActive);

            if (existingCategory == null)
            {
                return false;
            }

            existingCategory.Name = category.Name;
            existingCategory.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                category.IsActive = false;
                category.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}
