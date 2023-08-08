using Microsoft.AspNetCore.Mvc;
using TouchGrassCart.Application.Interface;
using TouchGrassCart.API.Mapping;
using TouchGrassCart.Contracts.Request;
using TouchGrassCart.Contracts.Response;

namespace TouchGrassCart.API.Controllers;


[ApiController]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    //GET all Products
    [HttpGet(ApiEndpoints.Products.GetAll)]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productRepository.GetProductsAsync();
        var productResponse = new FinalResponse<ProductsResponse>
        {
            StatusCode = 200,
            Message = "Products retrieved successfully.",
            Data = products.MapsToResponse()
        };
        return Ok(productResponse);
    }
    
    //GET ProductById
    [HttpGet(ApiEndpoints.Products.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var product = await _productRepository.GetProductById(id);
        if (product == null)
        {
            return NotFound(new FinalResponse<object>
            {
                StatusCode = 404,
                Message = "Product not found."
            });
        }
        
        var productResponse = new FinalResponse<ProductResponse>
        {
            StatusCode = 200,
            Message = "Product retrieved successfully.",
            Data = product.MapsToResponse()
        };
        return Ok(productResponse);
    }
    
    //POST Products
    [HttpPost(ApiEndpoints.Products.Create)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "Product data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }
        
        var product = request.MapToProduct();
        await _productRepository.CreateProduct(product ?? throw new InvalidOperationException());
        var productResponse = new FinalResponse<ProductResponse>
        {
            StatusCode = 200,
            Message = "Product created successfully.",
            Data = product.MapsToResponse()
        };
        return CreatedAtAction(nameof(Get), new { id = product.Id }, productResponse);
    }
    
    //UPDATE Product
    [HttpPut(ApiEndpoints.Products.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductRequest request)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "Product data is invalid." });
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }
        var product = request.MapToProduct(id);
        var updated = await _productRepository.UpdateProduct(product);
        if (updated is false)
        {
            return NotFound();
        }
        var response = new FinalResponse<ProductResponse>
        {
            StatusCode = 200,
            Message = "Product details updated successfully.",
            Data = product.MapsToResponse()
        };
        return Ok(response);

    }

    //DELETE Product 
    [HttpDelete(ApiEndpoints.Products.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productRepository.ProductExists(id);
        var deleteProduct = await _productRepository.DeleteProduct(id);
        if (!deleteProduct)
        {
            return NotFound(new FinalResponse<string>
            {
                StatusCode = 404,
                Message = "Product not found or already deleted",
                Data = null
            });
        }
        
        return Ok(new FinalResponse<string>
            {
                StatusCode = 200,
                Message = "Product deleted successfully",
                Data = null
            });
    }
}