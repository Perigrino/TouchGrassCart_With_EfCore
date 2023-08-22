namespace TouchGrassCart.Contracts.Response;

public class CartsResponse
{
    public required IEnumerable<CartResponse> Carts { get; init; } = Enumerable.Empty<CartResponse>();
}