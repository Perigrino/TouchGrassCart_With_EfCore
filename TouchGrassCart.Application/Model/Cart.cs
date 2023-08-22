namespace TouchGrassCart.Application.Model;

public class Cart
{
    public Guid Id { get; set; }
    
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}