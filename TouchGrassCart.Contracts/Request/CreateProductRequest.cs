namespace TouchGrassCart.Contracts.Request;

public class CreateProductRequest
{
    public required string ProductName { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int Quantity { get; set; }
}