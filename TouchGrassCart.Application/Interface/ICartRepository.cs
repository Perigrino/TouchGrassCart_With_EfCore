using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Application.Interface;

public interface ICartRepository
{
    Task<IEnumerable<Cart>> GetCartsAsync();
    Task<bool> CreateCart(Cart cart);
    Task<Cart> GetCartById(Guid cartId);
    Task<bool> DeleteCart(Guid cartId);
    Task<bool> AddToCart(CartItem addToCart);
    //Task<bool> UpdateCartItem(Guid cartItemId, int newQuantity);
    Task<bool> RemoveFromCart(Guid productId);
    Task<bool> Save();
}