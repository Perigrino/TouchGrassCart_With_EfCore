using TouchGrassCart.Application.Model;
using TouchGrassCart.Contracts.Request;
using TouchGrassCart.Contracts.Response;

namespace TouchGrassCart.API.Mapping;

public static class CartContractMapping
{
    public static Cart MapToCart(this CreateCartRequest request)
    {
        return new Cart
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId
        };
    
    }
    
    public static CartResponse MapsToResponse(this Cart cart)
    {
        return new CartResponse()
        {
            Id = Guid.NewGuid(),
            CustomerId = cart.CustomerId,
            Items = (List<CartItem>)cart.CartItems
        };
    }
    
    public static CartsResponse MapsToResponse(this IEnumerable<Cart> cart)
    {
        return new CartsResponse
        {
            Carts = cart.Select(MapsToResponse)
        };
    }
    
    public static Cart MapToCart(this UpdateCartRequest request)
    {
        return new Cart()
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId
        };
    }
}