using Microsoft.EntityFrameworkCore;
using TouchGrassCart.Application.Database.AppDbContext;
using TouchGrassCart.Application.Interface;
using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Application.Repository;

public class ProductRepository: IProductRepository
{
    
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var result = await _context.Products
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
        return result;
    }

    public async Task<Product?> GetProductById(Guid id)
    {
        var result = await _context.Products
            .FirstOrDefaultAsync(i => i.Id == id);
        return result;
    }
    

    public async Task<bool> CreateProduct(Product product)
    {
        var newProduct = new Product()
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            CreatedAt = product.CreatedAt
        };
        await _context.AddAsync(newProduct);
        return await Save();

    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var result = await _context.Products.FirstOrDefaultAsync(p => p.Id  == product.Id);
        if (result != null)
        {
            result.ProductName = product.ProductName;
            result.Description = product.Description;
            result.Price = product.Price;
            result.Quantity = product.Quantity;
            result.UpdatedAt = product.UpdatedAt;
        }
        return await Save();
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var result = await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
        if (result == null)
        {
            return false; // Product not found or already deleted
        }
        _context.Remove(result);
        return await Save();
    }

    public async Task<bool> ProductExists(Guid id)
    {
        var products = await _context.Products.AnyAsync(c => c.Id == id);
        return products;
    }

    public async Task<bool> Save()
    {
        var saved =  await _context.SaveChangesAsync();
        return saved > 0;
    }
}