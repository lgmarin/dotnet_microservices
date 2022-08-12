using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        CartViewModel? cartViewModel = await GetCartByUser();

        if (cartViewModel is null)
        {
            ModelState.AddModelError("CartNotFound", "Cart does not exist!");
            return View("/Views/Cart/CartNotFound.cshtml");
        }
        
        return View(cartViewModel);
    }
}
