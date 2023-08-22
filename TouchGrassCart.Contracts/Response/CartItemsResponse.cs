namespace TouchGrassCart.Contracts.Response;

public class CartItemsResponse
{
    public required IEnumerable<CartItemResponse> CartItems { get; init; } = Enumerable.Empty<CartItemResponse>();
}