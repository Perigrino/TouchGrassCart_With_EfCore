using TouchGrassCart.Application.Model;
using TouchGrassCart.Contracts.Request;
using TouchGrassCart.Contracts.Response;

namespace TouchGrassCart.API.Mapping;

public static class CustomerContractMappings
{
    public static Customer MapToCustomer(this CreateCustomerRequest request)
    {
        return new Customer()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            Address = request.Address
        };
    
    }
    
    public static CustomerResponse MapsToResponse(this Customer customer)
    {
        return new CustomerResponse()
        {
            CustomerId = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Gender = customer.Gender,
            Address = customer.Address
            
        };
    }
    
    public static CustomersResponse MapsToResponse(this IEnumerable<Customer> customers)
    {
        return new CustomersResponse()
        {
            Customers = customers.Select(MapsToResponse)
        };
    }
    
    public static Customer MapToCustomer(this UpdateCustomerRequest request, Guid id)
    {
        return new Customer()
        {
            Id = id,
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            Address = request.Address,
            UpdatedAt = request.UpdatedAt
        };
    }
}