using back_end.Application.Features.Products.Dtos;
using back_end.Application.Interfaces;
using back_end.Domain.Entities;
using back_end.Infastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace back_end.Infastructure.Repositories
{

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Where(c => c.IsActive).ToListAsync();
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            var existingProduct= await _context.Products
                                        .FirstOrDefaultAsync(c => c.Name == product.Name && c.IsActive);

            if (existingProduct!= null)
            {
                return false;
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var existingProduct= await _context.Products
                                      .FirstOrDefaultAsync(c => c.Id == product.Id && c.IsActive);

            if (existingProduct== null)
            {
                return false;
            }

            existingProduct.Name = product.Name;
            existingProduct.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product != null)
            {
                product.IsActive = false;
                product.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}
