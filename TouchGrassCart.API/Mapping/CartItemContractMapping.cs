using TouchGrassCart.Application.Model;
using TouchGrassCart.Contracts.Request;
using TouchGrassCart.Contracts.Response;

namespace TouchGrassCart.API.Mapping;

public static class CartItemContractMapping
{
    public static CartItem MapToCartItem(this CreateCartItemRequest request)
    {
        return new CartItem
        {
            Id = Guid.NewGuid(),
            CartId = request.CartId,
            ProductId = request.ProductId,
            ProductName = request.ProductName,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice
        };
    
    }
    
    public static CartItemResponse MapsToResponse(this CartItem cartItem)
    {
        return new CartItemResponse
        {
            Id = Guid.NewGuid(),
            CartId = cartItem.CartId,
            ProductId = cartItem.ProductId,
            ProductName = cartItem.ProductName,
            Quantity = cartItem.Quantity,
            UnitPrice = cartItem.UnitPrice
        };
    }
    
    public static CartItemsResponse MapsToResponse(this IEnumerable<CartItem> cartItems)
    {
        return new CartItemsResponse()
        {
            CartItems = cartItems.Select(MapsToResponse)
        };
    }
    
    public static CartItem MapToCartItem(this UpdateCartItemRequest request, Guid guid)
    {
        return new CartItem
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            ProductName = request.ProductName,
            CartId = request.CartId,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice
        };
    }
}