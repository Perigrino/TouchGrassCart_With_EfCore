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
    
    public static CartItem MapsToResponse(this CartItem cartItem)
    {
        return new CartItem
        {
            Id = Guid.NewGuid(),
            CartId = cartItem.CartId,
            ProductId = cartItem.ProductId,
            ProductName = cartItem.ProductName,
            Quantity = cartItem.Quantity,
            UnitPrice = cartItem.UnitPrice
        };
    }
    
    public static Cart MapsToResponse(this Cart cartItems)
    {
        return cartItems;
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