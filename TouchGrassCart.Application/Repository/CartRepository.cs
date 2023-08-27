using Microsoft.EntityFrameworkCore;
using TouchGrassCart.Application.Database.AppDbContext;
using TouchGrassCart.Application.Interface;
using TouchGrassCart.Application.Model;


namespace TouchGrassCart.Application.Repository;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cart>> GetCartsAsync()
    {
        var carts = await _context.Carts
            .Include(cartItems => cartItems.CartItems)
            .ToListAsync();
        return carts;
    }

    public async Task<bool> CreateCart(Cart cart)
    {
        var newCart = new Cart
        {
            Id = new Guid(),
            CustomerId = cart.CustomerId,
            Customer = null
        };
        await _context.AddAsync(newCart);
        return await Save();
    }

    public async Task<Cart> GetCartById(Guid cartId)
    {
            var result = await _context.Carts
            .Include(cartItems => cartItems.CartItems)
            .FirstOrDefaultAsync(i => i.Id == cartId);
        return result ?? throw new InvalidOperationException();
    }


    public async Task<bool> DeleteCart(Guid cartId)
    {
        var result = await _context.Carts.FirstOrDefaultAsync(i => i.Id == cartId);
        if (result == null)
        {
            return false; // Cart not found or already deleted
        }
        _context.Remove(result);
        return await Save();
    }
    

    public async Task<bool> AddToCart(CartItem cartItem)
    {
        var existingItem = await _context.CartItems.FirstOrDefaultAsync(item => item.ProductId == cartItem.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += cartItem.Quantity;
        }
        else
        {
            _context.CartItems.Add(new CartItem
            {
                Id = Guid.NewGuid(),
                ProductId = cartItem.ProductId,
                CartId = cartItem.CartId,
                ProductName = cartItem.ProductName,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.UnitPrice
            });
        }
        return await Save();
    }

    // public async Task<bool> UpdateCartItem(Guid cartItemId, int newQuantity)
    // {
    //     var cartItem = await _context.CartItems.FirstOrDefaultAsync(item => item.Id == cartItemId);
    //
    //     if (cartItem == null)
    //     {
    //         return false; // Cart item not found
    //     }
    //     cartItem.Quantity = newQuantity;
    //     return await Save();
    // }
    
    public async Task<bool> RemoveFromCart (Guid productId)
    {
        var cartItem = await _context.CartItems.FirstOrDefaultAsync(item => item.ProductId == productId);
    
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
        }
        return await Save();
    }
    
    public async Task<bool> Save()
    {
        var saved =  await _context.SaveChangesAsync();
        return saved > 0;
    }
}