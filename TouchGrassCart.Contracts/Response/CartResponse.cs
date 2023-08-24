using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Contracts.Response;

public class CartResponse
{
    public required Guid Id { get; set; }
    public required Guid CustomerId { get; set; }
    //public required decimal TotalAmount { get; set; }
    public required List<CartItemResponse> Items { get; set; } = new List<CartItemResponse>();
}