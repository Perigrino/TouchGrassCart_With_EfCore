using Microsoft.AspNetCore.Mvc;
using TouchGrassCart.Application.Interface;
using TouchGrassCart.Application.Model;
using TouchGrassCart.Contracts.Response;

namespace TouchGrassCart.API.Controllers;

[ApiController]
public class CartController : Controller
{
    private readonly ICartRepository _cartRepository;

    public CartController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    //POST Cart
    [HttpPost(ApiEndpoints.Cart.CreateCart)]
    public async Task<IActionResult> CreateCart([FromBody] Cart request)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400, Message = "Customer data is invalid." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object>
                { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }

        //var cart = request.MapToCart();
        var newCart = await _cartRepository.CreateCart(request);
        if (newCart)
        {
            var cartResponses = new FinalResponse<Cart>
            {
                StatusCode = 201,
                Message = "Cart has been created successfully.",
                Data = null
            };
            return CreatedAtAction(nameof(CreateCart), cartResponses);
        }

        var cartResponse = new FinalResponse<Cart>
        {
            StatusCode = 400,
            Message = "Cart not found",
            Data = null
        };
        return CreatedAtAction(nameof(CreateCart), cartResponse);
    }

    [HttpGet(ApiEndpoints.Cart.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var cart = await _cartRepository.GetCatById(id);
        if (cart == null)
        {
            return NotFound(new FinalResponse<object>
            {
                StatusCode = 404,
                Message = "Cart Items not found."
            });
        }

        var productResponse = new FinalResponse<object>
        {
            StatusCode = 200,
            Message = "Cart Items retrieved successfully.",
            Data = cart
        };
        return Ok(productResponse);
    }

    //DELETE Cart 
    [HttpDelete(ApiEndpoints.Cart.DeleteCart)]
    public async Task<IActionResult> DeleteCart(Guid id)
    {
        var deleteCustomer = await _cartRepository.DeleteCart(id);
        if (!deleteCustomer)
        {
            return NotFound(new FinalResponse<string>
            {
                StatusCode = 404,
                Message = "Cart not found or already deleted",
                Data = null
            });
        }

        return Ok(new FinalResponse<string>
        {
            StatusCode = 200,
            Message = "Cart deleted successfully",
            Data = null
        });
    }
}



//ADD To Cart 
    // [HttpPost(ApiEndpoints.Cart.AddToCart)]
    // public IActionResult AddToCart([FromBody] CartItem? request)
    // {
    //     if (request == null)
    //     {
    //         return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "Customer data is invalid." });
    //     }
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
    //     }
    //     var cartItem = request.MapsToResponse();
    //     _cartRepository.AddToCart(request);
    //     var cartResponse = new FinalResponse<CartItemResponse>
    //     {
    //         StatusCode = 200,
    //         Message = "Cart Item has been added successfully.",
    //         Data = cartItem
    //     };
    //     return CreatedAtAction(nameof(AddToCart), new { id = cartItem.Id}, cartResponse);
    //}
    
    //UPDATE Cart 
    // [HttpPut(ApiEndpoints.Cart.Update)]
    // public IActionResult UpdateCart([FromBody] CartItem? request)
    // {
    //     if (request == null)
    //     {
    //         return BadRequest(new FinalResponse<object>() { StatusCode = 400,Message = "Customer data is invalid." });
    //     }
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(new FinalResponse<object> { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
    //     }
    //     var cartItem = request.MapsToResponse();
    //     _cartRepository.UpdateCartItem(request);
    //     var cartResponse = new FinalResponse<CartItemResponse>
    //     {
    //         StatusCode = 200,
    //         Message = "Quantity Item has been updated successfully.",
    //         Data = cartItem
    //     };
    //     return CreatedAtAction(nameof(UpdateCart), new { id = cartItem.Id}, cartResponse);
    //}
    
    //DELETE Remove Cart Item 
    // [HttpDelete(ApiEndpoints.Cart.RemoveCartItem)]
    // public async Task<IActionResult> RemoveCartItem (Guid id)
    // {
    //     var deleteCustomer = await _cartRepository.RemoveFromCart(id);
    //     if (!deleteCustomer)
    //     {
    //         return NotFound(new FinalResponse<string>
    //         {
    //             StatusCode = 404,
    //             Message = "Cart Item not found or already deleted",
    //             Data = null
    //         });
    //     }
    //     
    //     return Ok(new FinalResponse<string>
    //     {
    //         StatusCode = 200,
    //         Message = "Cart Item deleted successfully",
    //         Data = null
    //     });
    // }