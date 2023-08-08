using System.Text.RegularExpressions;

namespace TouchGrassCart.Application.Model;

public partial class Product
{
    public Guid Id { get; set; }
    public required string ProductName { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    
}