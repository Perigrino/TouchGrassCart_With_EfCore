using TouchGrassCart.Application.Model;
using TouchGrassCart.Contracts.Request;
using TouchGrassCart.Contracts.Response;

namespace TouchGrassCart.API.Mapping;

public static class ContractMappings
{
    public static Product? MapToProduct(this CreateProductRequest request)
    {
        return new Product()
        {
            Id = Guid.NewGuid(),
            ProductName = request.ProductName,
            Description = request.Description,
            Quantity = request.Quantity,
            Price = request.Price
        };
    
    }
    
    public static ProductResponse MapsToResponse(this Product product)
    {
        return new ProductResponse()
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            CreatedAt = product.CreatedAt
        };
    }
    
    public static ProductsResponse MapsToResponse(this IEnumerable<Product> products)
    {
        return new ProductsResponse()
        {
            Items = products.Select(MapsToResponse)
        };
    }
    
    public static Product MapToProduct(this UpdateProductRequest request, Guid id)
    {
        return new Product()
        {
            Id = id,
            ProductName = request.ProductName,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity
        };
    }
}