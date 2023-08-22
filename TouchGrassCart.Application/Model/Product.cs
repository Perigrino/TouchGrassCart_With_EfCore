using System.Text.RegularExpressions;

namespace TouchGrassCart.Application.Model;

public partial class Product
{
    public Guid Id { get; set; }
    public required string ProductName { get; set; }
    public required string Description { get; set; }
    public required double UnitPrice { get; set; }
    public required int StockNumber { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    
}