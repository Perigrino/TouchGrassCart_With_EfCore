namespace TouchGrassCart.Application.Model;

public class Customer
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Gender { get; set; }
    public required string Address { get; set; }
    public List<Cart?> Carts { get; set; } = new List<Cart>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}