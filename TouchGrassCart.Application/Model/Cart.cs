using System.Text.Json.Serialization;

namespace TouchGrassCart.Application.Model;

public class Cart
{
    public Guid Id { get; set; } = Guid.NewGuid(); //.ToString("N");
    public Guid CustomerId { get; set; }
    [JsonIgnore]
    public Customer? Customer { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}