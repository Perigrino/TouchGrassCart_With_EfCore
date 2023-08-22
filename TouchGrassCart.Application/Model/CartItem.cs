using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TouchGrassCart.Application.Model;

public class CartItem
{
    public required Guid Id { get; set; }
    public required Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public required int Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
    public required Guid CartId { get; set; } // Foreign key to link the cart item to a specific cart
    [JsonIgnore]
    public Cart Cart { get; set; } // Navigation property to access the cart
}