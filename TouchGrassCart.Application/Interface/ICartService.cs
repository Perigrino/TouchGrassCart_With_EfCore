using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Application.Interface;

public interface ICartService
{
    Task<decimal> CalculateTotalAsync(Guid cartId);
}