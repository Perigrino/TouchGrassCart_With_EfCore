using Microsoft.EntityFrameworkCore;
using TouchGrassCart.Application.Interface;
using TouchGrassCart.Application.Model;
using TouchGrassCart.Application.Database.AppDbContext;

namespace TouchGrassCart.Application.Repository;

public class CustomerRepository: ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetCustomerAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        return customers;
    }

    public async Task<Customer?> GetCustomerById(Guid id)
    {
        var result = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        return result;
    }

    public async Task<bool> CreateCustomer(Customer customer)
    {
        var newCustomer = new Customer()
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            Email = customer.Email,
            Gender = customer.Gender,
            PhoneNumber = customer.PhoneNumber,
            CreatedAt = customer.CreatedAt,
        };
        await _context.AddAsync(newCustomer);
        return await Save();
    }

    public async Task<bool> UpdateCustomer(Customer customer)
    {
        var result = await _context.Customers.FirstOrDefaultAsync(p => p.Id  == customer.Id);
        if (result != null)
        {
            result.Name = customer.Name;
            result.Address = customer.Address;
            result.Email = customer.Email;
            result.Gender = customer.Gender;
            result.PhoneNumber = customer.PhoneNumber;
            result.UpdatedAt = customer.UpdatedAt;
        }
        return await Save();
    }

    public async Task<bool> DeleteCustomer(Guid id)
    {
        var result = await _context.Customers.FirstOrDefaultAsync(i => i.Id == id);
        if (result == null)
        {
            return false; // Product not found or already deleted
        }
        _context.Remove(result);
        return await Save();
    }

    public async Task<bool> CustomerExists(Guid id)
    {
        var products =  await _context.Customers.AnyAsync(c => c.Id == id);
        return products;
    }
    
    public async Task<bool> Save()
    {
        var saved =  await _context.SaveChangesAsync();
        return saved > 0;
    }
}