using back_end.Application.Features.Products.Dtos;
using back_end.Domain.Entities;

namespace back_end.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<bool> AddProductAsync(Product category);
        Task<bool> UpdateProductAsync(Product category);
        Task<bool> DeleteProductAsync(int id);
    }
}
