using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Application.Interface;

public interface ICartItemRepository
{
    Task<IEnumerable<CartItem>> GetCartItemAsync(); //Get all cart items
    Task<bool> CreateCartItem(CartItem cartItem); //Creates a new cart item
    //Task<bool> UpdateCartItem(CartItem cartItem); //Updates a cart item
    Task<bool> RemoveCartItem(Guid cartItemId); //Removes a cart item

    Task<bool> CartItemExists(Guid cartItemId); //Checks it item exists
    Task<bool> Save(); //Saves to Db
}