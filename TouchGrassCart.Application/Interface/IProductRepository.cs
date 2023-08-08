using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Application.Interface;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product?> GetProductById(Guid id);
    Task<bool> CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Guid id);
    Task<bool> ProductExists(Guid id);
    Task<bool> Save();
}