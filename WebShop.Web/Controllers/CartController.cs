using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ICouponService _couponService;

    public CartController(ICartService cartService, ICouponService couponService)
    {
        _cartService = cartService;
        _couponService = couponService;
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

    public async Task<CartViewModel> GetCartByUser()
    {
        var cart = await _cartService.GetCartByUserId(GetUserId(), await GetAccessToken());

        if (cart?.CartHeader is not null)
        {
            if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
            {
                var coupon = await _couponService.GetCouponByCode(cart.CartHeader.CouponCode, await GetAccessToken());

                if (coupon.CouponCode is not null)
                {
                    cart.CartHeader.Discount = coupon.Discount;
                }
            }
            foreach (var item in cart.CartItems)
            {
                cart.CartHeader.TotalAmount += (item.Product.Price * item.Quantity);
            }
            
            cart.CartHeader.TotalAmount = cart.CartHeader.TotalAmount - (cart.CartHeader.TotalAmount * cart.CartHeader.Discount)/100;
        }

        return cart;
    }

    public async Task<IActionResult> RemoveItem(int id)
    {
        var result = await _cartService.RemoveItemFromCart(id, await GetAccessToken());

        if (result)
        {
            return RedirectToAction(nameof(Index));
        }
        
        return View(id);
    }

    [HttpPost]
    public async Task<IActionResult> ApplyCoupon(CartViewModel cartViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _cartService.ApplyCoupon(cartViewModel, await GetAccessToken());

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        return RedirectToAction(nameof(Index));
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteCoupon()    
    {
        if (ModelState.IsValid)
        {
            var result = await _cartService.RemoveCoupon(GetUserId(), await GetAccessToken());

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var cartViewModel = await GetCartByUser();
        return View(cartViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CartViewModel cartViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _cartService.Checkout(cartViewModel.CartHeader, await GetAccessToken());

            if (result is not null)
            {
                return RedirectToAction(nameof(CheckoutCompleted));
            }
        }

        return View(cartViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> CheckoutCompleted()
    {
        
    }



    private string GetUserId()
    {
        return User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
    }

    private async Task<String> GetAccessToken()
    {
        return await HttpContext.GetTokenAsync("access-token");
    }
}
