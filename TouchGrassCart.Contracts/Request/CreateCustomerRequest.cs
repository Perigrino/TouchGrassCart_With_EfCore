namespace TouchGrassCart.Contracts.Request;

public class CreateCustomerRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Gender { get; set; }
    public required string Address { get; set; }
}