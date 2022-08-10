using Microsoft.AspNetCore.Mvc;
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
}