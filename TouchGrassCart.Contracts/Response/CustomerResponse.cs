namespace TouchGrassCart.Contracts.Response;

public class CustomerResponse
{
    public required Guid CustomerId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required int PhoneNumber { get; set; }
    public required string Gender { get; set; }
    public required string Address { get; set; }
}