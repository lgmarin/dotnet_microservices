using Microsoft.AspNetCore.Mvc;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    public IActionResult Index()
    {
        
        return View();
    }
}
