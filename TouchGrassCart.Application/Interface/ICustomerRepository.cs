using TouchGrassCart.Application.Model;

namespace TouchGrassCart.Application.Interface;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetCustomerAsync();
    Task<Customer?> GetCustomerById(Guid id);
    Task<bool> CreatePCustomer(Customer customer);
    Task<bool> UpdateCustomer(Customer customer);
    Task<bool> DeleteCustomer(Guid id);
    Task<bool> CustomerExists(Guid id);
}