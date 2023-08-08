namespace TouchGrassCart.Application.Model;

public class CartItem
{
    public required Guid CartItemId { get; set; }
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public int CartId { get; set; } // Foreign key to link the cart item to a specific cart
    public Cart Cart { get; set; } // Navigation property to access the cart
}