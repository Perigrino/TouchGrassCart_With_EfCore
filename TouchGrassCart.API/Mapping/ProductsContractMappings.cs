using TouchGrassCart.Application.Model;
using TouchGrassCart.Contracts.Request;
using TouchGrassCart.Contracts.Response;

namespace TouchGrassCart.API.Mapping;

public static class ProductsContractMappings
{
    public static Product MapToProduct(this CreateProductRequest request)
    {
        return new Product()
        {
            Id = Guid.NewGuid(),
            ProductName = request.ProductName,
            Description = request.Description,
            StockNumber = request.StockNumber,
            UnitPrice = request.UnitPrice
        };
    
    }
    
    public static ProductResponse MapsToResponse(this Product product)
    {
        return new ProductResponse()
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Description = product.Description,
            UnitPrice = product.UnitPrice,
            StockNumber = product.StockNumber,
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
            UnitPrice = request.UnitPrice,
            StockNumber = request.StockNumber,
            UpdatedAt = request.UpdatedAt
        };
    }
}