using System.ComponentModel.DataAnnotations;
using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Contracts.Response;

public class CartResponse
{
    public required Guid Id { get; set; }
    public required Guid CustomerId { get; set; }
    //public required decimal TotalAmount { get; set; }
    public required List<CartItem> Items { get; set; } = new List<CartItem>();
}