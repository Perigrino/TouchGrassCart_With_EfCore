using System.ComponentModel.DataAnnotations;
using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Contracts.Request;

public class CreateCartRequest
{
    public required Guid CustomerId { get; set; }
    //public required List<CartItem> Items { get; set; } = new List<CartItem>();
}

// public class AddToCartRequest
// {
//     public required Guid ProductId { get; set; }
//     public required Guid CartId { get; set; }
//     public required string ProductName { get; set; }
//     public required int Quantity { get; set; }
//     public required decimal Price { get; set; }
// }