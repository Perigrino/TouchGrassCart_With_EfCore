namespace TouchGrassCart.Contracts.Response;

public class ProductsResponse
{
    public required IEnumerable<ProductResponse> Items { get; init; } = Enumerable.Empty<ProductResponse>();
}