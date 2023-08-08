namespace TouchGrassCart.Application.Model;

public class Customer
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required int PhoneNumber { get; set; }
    public required string Gender { get; set; }
    public required string Address { get; set; }
    public List<Cart?> Carts { get; set; } = new List<Cart>();

}