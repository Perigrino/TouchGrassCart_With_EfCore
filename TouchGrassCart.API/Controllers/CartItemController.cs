using Microsoft.AspNetCore.Mvc;
using TouchGrassCart.API.Mapping;
using TouchGrassCart.Application.Interface;
using TouchGrassCart.Application.Model;
using TouchGrassCart.Contracts.Request;
using TouchGrassCart.Contracts.Response;

namespace TouchGrassCart.API.Controllers;

[ApiController]
public class CartItemController : Controller
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemController(ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    //GET all CartItems
    [HttpGet(ApiEndpoints.CartItem.GetAll)]
    public async Task<IActionResult> GetCartItems()
    {
        var cartItem = await _cartItemRepository.GetCartItemAsync();
        var cartItemResponse = new FinalResponse<List<CartItem>>
        {
            StatusCode = 200,
            Message = "Cart Items retrieved successfully.",
            Data = cartItem.ToList()
        };
        return Ok(cartItemResponse);
    }

    // //POST CartItem
    // [HttpPost(ApiEndpoints.CartItem.Create)]
    // public async Task<IActionResult> CreateCartItem([FromBody] CreateCartItemRequest request)
    // {
    //     if (request == null)
    //     {
    //         return BadRequest(new FinalResponse<object>() { StatusCode = 400, Message = "Cart Item data is invalid." });
    //     }
    //
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(new FinalResponse<object>
    //             { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
    //     }
    //
    //     var cartItem = request.MapToCartItem();
    //     await _cartItemRepository.CreateCartItem(cartItem ?? throw new InvalidOperationException());
    //     var customerResponse = new FinalResponse<CartItemResponse>
    //     {
    //         StatusCode = 200,
    //         Message = "Cart Item created successfully.",
    //         Data = cartItem
    //     };
    //     return CreatedAtAction(nameof(GetCartItems), new { id = cartItem.Id }, customerResponse);
    //
    // }
    
    
    //UPDATE CartItem
    [HttpPut(ApiEndpoints.CartItem.Update)]
    public async Task<IActionResult> UpdateCartItem([FromRoute] Guid id, [FromBody] UpdateCartItemRequest request)
    {
        if (request == null)
        {
            return BadRequest(new FinalResponse<object>() { StatusCode = 400, Message = "Cart Item data is invalid." });
        }

        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(new FinalResponse<object>
        //         { StatusCode = 400, Message = "Validation failed.", Data = ModelState });
        // }

        var cartItem = request.MapToCartItem(id);
        var updated = await _cartItemRepository.UpdateCartItem(cartItem);
        if (updated is false)
        {
            return NotFound();
        }

        var response = new FinalResponse<object>
        {
            StatusCode = 200,
            Message = "Cart Item details updated successfully.",
            Data = cartItem.MapsToResponse()
        };
        return Ok(response);
    }

    //DELETE CartItem 
        [HttpDelete(ApiEndpoints.CartItem.Delete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cartItemRepository.CartItemExists(id);
            var deleteCartItem = await _cartItemRepository.RemoveCartItem(id);
            if (!deleteCartItem)
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