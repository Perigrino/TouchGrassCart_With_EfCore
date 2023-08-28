using Microsoft.EntityFrameworkCore;
using TouchGrassCart.Application.Database.AppDbContext;
using TouchGrassCart.Application.Interface;
using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Application.Services;

public class CartService: ICartService
{
    private readonly AppDbContext _context;
    private readonly ICartRepository _cartRepository;
    public CartService(AppDbContext context, ICartRepository cartRepository)
    {
        _context = context;
        _cartRepository = cartRepository;
    }

    public async Task<decimal> CalculateTotalAsync(Guid cartId)
    {
        decimal totalAmount = 0;
        var cartItems = await _cartRepository.GetCartById(cartId);
        var items = cartItems.CartItems.ToList();
        foreach (CartItem cartItem in items)
        {
            totalAmount += cartItem.Quantity * cartItem.UnitPrice;
        }
        return totalAmount;
    }
}