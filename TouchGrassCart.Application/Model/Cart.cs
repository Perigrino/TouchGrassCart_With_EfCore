namespace TouchGrassCart.Application.Model;

public class Cart
{
    public required int CartId { get; set; }
    //public required int CustomerId { get; set; }
    public required List<CartItem> Items { get; set; } = new List<CartItem>();
    
    public Customer Customer { get; set; } // Navigation property to access the customer
}