﻿using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProducts(string.Empty);

        if (products is null)
            return View("Error");
        
        return View();
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
    {
        var token = await HttpContext.GetTokenAsync("access-token");
        var product = await _productService.FindProductById(id, token);

        if (product is null)
            return View("Error");
        
        return View(product);
    }

    [HttpPost]
    [ActionName("ProductDetails")]
    [Authorize]
    public async Task<ActionResult<ProductViewModel>> ProductDetailsPost(ProductViewModel productViewModel)
    {
        var token = await HttpContext.GetTokenAsync("access-token");

        CartViewModel cart = new()
        {
            CartHeader = new CartHeaderViewModel
            {
                UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
            }

        };

        CartItemViewModel cartItem = new()
        {
            Quantity = productViewModel.Quantity,
            ProductId = productViewModel.Id,
            Product = await _productService.FindProductById(productViewModel.Id, token)
        };

        List<CartItemViewModel> cartItemViewModels = new List<CartItemViewModel>();
        cartItemViewModels.Add(cartItem);
        cart.CartItems = cartItemViewModels;

        var result = await _cartService.AddItemToCart(cart, token);

        if (result is not null)
            return RedirectToAction(nameof(Index));

        return View(productViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [Authorize]
    public async Task<IActionResult> Login()
    {
        var accessToken = await HttpContext.GetTokenAsync("access-token");
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }

}