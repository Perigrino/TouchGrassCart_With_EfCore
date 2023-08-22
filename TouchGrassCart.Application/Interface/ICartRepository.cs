using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Application.Interface;

public interface ICartRepository
{
    Task<bool> CreateCart(Cart cart);
    Task<Cart> GetCatById(Guid cartId);
    Task<bool> DeleteCart(Guid cartId);
    Task<bool> AddToCart(CartItem addToCart);
    Task<bool> UpdateCartItem(CartItem updateCart);
    Task<bool> RemoveFromCart(Guid productId);
    Task<bool> Save();
}