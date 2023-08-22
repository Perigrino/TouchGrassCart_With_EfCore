using System.ComponentModel.DataAnnotations;
using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Contracts.Request;

public class UpdateCartRequest
{
    public required Guid Id { get; set; }
    public required Guid CustomerId { get; set; }
}