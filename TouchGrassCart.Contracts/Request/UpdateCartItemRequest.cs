namespace TouchGrassCart.Contracts.Request;

public class UpdateCartItemRequest
{
    public required Guid Id { get; set; }
    public Guid CartId { get; set; }
    public required Guid ProductId { get; set; }
    public required string ProductName { get; set; }
    public required int Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
}