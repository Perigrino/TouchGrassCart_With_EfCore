using Mapster;
using Microsoft.AspNetCore.Mvc;
using TouchGrassCart.Application.Interface;
using TouchGrassCart.Application.Model;
using TouchGrassCart.Contracts.Request;
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
    
    
    //GET all Carts
    [HttpGet(ApiEndpoints.Cart.GetAll)]
    public async Task<IActionResult> GetCarts()
    {
        var cart = await _cartRepository.GetCartsAsync();
        var cartResponse = new FinalResponse<object>
        {
            StatusCode = 200,
            Message = "Carts retrieved successfully.",
            Data = cart
        };
        return Ok(cartResponse);
    }

    //POST Cart
    [HttpPost(ApiEndpoints.Cart.CreateCart)]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request)
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
        
        var newCart = await _cartRepository.CreateCart(request.Adapt<Cart>());
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
        var cart = await _cartRepository.GetCartById(id);
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

//ADD To Cart 
    [HttpPost(ApiEndpoints.Cart.AddToCart)]
    public IActionResult AddToCart([FromBody] CreateCartItemRequest request)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400, Message = "Cart item data is invalid." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(new FinalResponse<object>
                { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        }
        
        var cartItem = _cartRepository.AddToCart(request.Adapt<CartItem>());
        var cartResponse = new FinalResponse<object>
        {
            StatusCode = 200,
            Message = "Cart Item has been added successfully.",
            Data = cartItem
        };
        return CreatedAtAction(nameof(AddToCart), cartResponse);
    }

    // //UPDATE Cart 
    // [HttpPut(ApiEndpoints.Cart.Update)]
    // public IActionResult UpdateCart(Guid cartItemId, int newQuantity)
    // {
    //     if (cartItemId == Guid.Empty)
    //     {
    //         return BadRequest(new FinalResponse<object>()
    //         {
    //             StatusCode = 400,
    //             Message = "Cart item ID is invalid."
    //         });
    //     }
    //
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(new FinalResponse<object>
    //         {
    //             StatusCode = 400,
    //             Message = "Validation failed.",
    //             Data = ModelState
    //         });
    //     }
    //     
    //     var updateResult = _cartRepository.UpdateCartItem(cartItemId, newQuantity);
    //     if (updateResult is null)
    //     {
    //         return NotFound(new FinalResponse<object>
    //         {
    //             StatusCode = 404,
    //             Message = "Cart item not found."
    //         });
    //     }
    //
    //     var response = new FinalResponse<object>
    //     {
    //         StatusCode = 200,
    //         Message = "Cart item quantity has been updated successfully."
    //     };
    //
    //     return Ok(response);
    // }

    //DELETE Remove Cart Item 
    
    [HttpDelete(ApiEndpoints.Cart.RemoveCartItem)]
    public async Task<IActionResult> RemoveCartItem(Guid id)
    {
        var deleteCustomer = await _cartRepository.RemoveFromCart(id);
        if (!deleteCustomer)
        {
            return NotFound(new FinalResponse<string>
            {
                StatusCode = 404,
                Message = "Cart Item not found or already deleted",
                Data = null
            });
        }

        return Ok(new FinalResponse<string>
        {
            StatusCode = 200,
            Message = "Cart Item deleted successfully",
            Data = null
        });
    }
}