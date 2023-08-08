using Microsoft.AspNetCore.Mvc;
using TouchGrassCart.API.Mapping;
using TouchGrassCart.Application.Interface;
using TouchGrassCart.Contracts.Request;
using TouchGrassCart.Contracts.Response;

namespace TouchGrassCart.API.Controllers;


[ApiController]
public class CustomerController :Controller
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    //GET all Customers
    [HttpGet(ApiEndpoints.Customers.GetAll)]
    public async Task<IActionResult> GetCustomers()
    {
        var customer = await _customerRepository.GetCustomerAsync();
        var customerResponse = new FinalResponse<CustomersResponse>
        {
            StatusCode = 200,
            Message = "Customers retrieved successfully.",
            Data = customer.MapsToResponse()
        };
        return Ok(customerResponse);
    }
    
    //GET CustomerById
    [HttpGet(ApiEndpoints.Customers.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var customer = await _customerRepository.GetCustomerById(id);
        if (customer == null)
        {
            return NotFound(new FinalResponse<object>
            {
                StatusCode = 404,
                Message = "Customer not found."
            });
        }
        
        var customerResponse = new FinalResponse<CustomerResponse>
        {
            StatusCode = 200,
            Message = "Customer retrieved successfully.",
            Data = customer.MapsToResponse()
        };
        return Ok(customerResponse);
    }
    
    //POST Customer
    [HttpPost(ApiEndpoints.Customers.Create)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "Customer data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }
        
        var customer = request.MapToCustomer();
        await _customerRepository.CreateCustomer(customer ?? throw new InvalidOperationException());
        var customerResponse = new FinalResponse<CustomerResponse>
        {
            StatusCode = 200,
            Message = "Customer created successfully.",
            Data = customer.MapsToResponse()
        };
        return CreatedAtAction(nameof(Get), new { id = customer.Id }, customerResponse);
    }
    
    //UPDATE Customer
    [HttpPut(ApiEndpoints.Customers.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCustomerRequest request)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "Customer data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }
        var customer = request.MapToCustomer(id);
        var updated = await _customerRepository.UpdateCustomer(customer);
        if (updated is false)
        {
            return NotFound();
        }
        var response = new FinalResponse<CustomerResponse>
        {
            StatusCode = 200,
            Message = "Customer details updated successfully.",
            Data = customer.MapsToResponse()
        };
        return Ok(response);

    }

    //DELETE Customer 
    [HttpDelete(ApiEndpoints.Customers.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _customerRepository.CustomerExists(id);
        var deleteCustomer = await _customerRepository.DeleteCustomer(id);
        if (!deleteCustomer)
        {
            return NotFound(new FinalResponse<string>
            {
                StatusCode = 404,
                Message = "Customer not found or already deleted",
                Data = null
            });
        }
        
        return Ok(new FinalResponse<string>
            {
                StatusCode = 200,
                Message = "Customer deleted successfully",
                Data = null
            });
    }
}