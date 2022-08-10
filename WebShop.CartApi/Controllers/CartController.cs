using Microsoft.AspNetCore.Mvc;
using WebShop.CartApi.DTOs;
using WebShop.CartApi.Repositories;

namespace WebShop.CartApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private ICartRepository _repository;


    public CartController(ICartRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<CartDTO>> GetById(string id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDto)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteCart(int id)
    {
        throw new NotImplementedException();
    }
}