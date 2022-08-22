using Microsoft.AspNetCore.Mvc;
using WebShop.CartApi.DTOs;
using WebShop.CartApi.Repositories;

namespace WebShop.CartApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _repository;


    public CartController(ICartRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("getcart/{id}")]
    public async Task<ActionResult<CartDTO>> GetByUserId(string userId)
    {
        var cart = await _repository.GetCartByUserId(userId);
        
        if (cart is null)
            return NotFound();

        return Ok(cart);
    }

    [HttpPost("addcart")]
    public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDto)
    {
        var cart = await _repository.UpdateCart(cartDto);

        if (cart is null)
            return NotFound();

        return Ok(cart);    
    }

    [HttpPut("updatecart")]
    public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDto)
    {
        var cart = await _repository.UpdateCart(cartDto);

        if (cart is null)
            return NotFound();

        return Ok(cart);
    }

    [HttpDelete("deletecart/{id}")]
    public async Task<ActionResult<bool>> DeleteCart(int id)
    {
        var status = await _repository.DeleteItemCart(id);

        if (!status)
            return BadRequest();

        return Ok(status);
    }

    [HttpPost("applycoupon")]
    public async Task<ActionResult<CartDTO>> ApplyCoupon(CartDTO cartDto)
    {
        var result = await _repository.ApplyCoupon(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);

        if (!result)
        {
            return NotFound($"CartHeader not found for userId: {cartDto.CartHeader.UserId}");
        }

        return Ok(result);
    }

    [HttpDelete("deletecoupon/{userId}")]
    public async Task<ActionResult<CartDTO>> DeleteCoupon(string userId)
    {
        var result = await _repository.DeleteCoupon(userId);

        if (!result)
        {
            return NotFound($"Coupon not found for userId: {userId}");
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CheckoutHeaderDTO>> Checkout(CheckoutHeaderDTO checkoutHeaderDto)
    {
        var cart = await _repository.GetCartByUserId(checkoutHeaderDto.UserId);

        if (cart is null)
        {
            return NotFound($"Cart not found for userId: {checkoutHeaderDto.UserId}");
        }

        checkoutHeaderDto.CartItems = cart.CartItems;
        checkoutHeaderDto.DateTime = DateTime.Now;

        return Ok(checkoutHeaderDto);
    }
}