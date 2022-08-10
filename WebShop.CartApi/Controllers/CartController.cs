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

        return Ok(cart);    }

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
}