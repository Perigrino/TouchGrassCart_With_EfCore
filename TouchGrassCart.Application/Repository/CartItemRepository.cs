using Microsoft.EntityFrameworkCore;
using TouchGrassCart.Application.Database.AppDbContext;
using TouchGrassCart.Application.Interface;
using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Application.Repository;

public class CartItemRepository : ICartItemRepository
{
    private readonly AppDbContext _context;

    public CartItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CartItem>> GetCartItemAsync()
    {
        var results = await _context.CartItems.ToListAsync();
        return results;
    }

    public async Task<bool> CreateCartItem(CartItem cartItem)
    {
        var newCartItem = new CartItem()
        {
            Id = cartItem.Id,
            ProductId = cartItem.ProductId,
            ProductName = cartItem.ProductName,
            Quantity = cartItem.Quantity,
            UnitPrice = cartItem.UnitPrice,
            CartId = cartItem.CartId
        };
        await _context.AddAsync(newCartItem);
        return await Save();
    }

    public async Task<bool> UpdateCartItem(CartItem cartItem)
    {
        var result = await _context.CartItems.FirstOrDefaultAsync(c => c.Id  == cartItem.Id);
        if (result != null)
        {
            result.Id = cartItem.Id;
            result.ProductId = cartItem.ProductId;
            result.Quantity = cartItem.Quantity;
            result.UnitPrice = cartItem.UnitPrice;
            result.CartId = cartItem.CartId;
        }
        return await Save();
    }

    public async Task<bool> RemoveCartItem(Guid cartItemId)
    {
        var result = await _context.CartItems.FirstOrDefaultAsync(i => i.Id == cartItemId);
        if (result == null)
        {
            return false; // CartItem not found or already deleted
        }
        _context.Remove(result);
        return await Save();
    }

    public async Task<bool> CartItemExists(Guid cartItemId)
    {
        var cartItem =  await _context.CartItems.AnyAsync(c => c.Id == cartItemId);
        return cartItem;
    }

    public async Task<bool> Save()
    {
        var saved =  await _context.SaveChangesAsync();
        return saved > 0;
    }
}