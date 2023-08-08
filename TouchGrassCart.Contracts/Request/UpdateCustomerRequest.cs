namespace TouchGrassCart.Contracts.Request;

public class UpdateCustomerRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Gender { get; set; }
    public required string Address { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}