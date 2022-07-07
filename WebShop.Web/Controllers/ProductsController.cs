using Microsoft.AspNetCore.Mvc;
using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var result = await _productService.GetAllProducts();

        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }

    public IActionResult CreateProduct()
    {
        throw new NotImplementedException();
    }

    public IActionResult UpdateProduct()
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteProduct()
    {
        throw new NotImplementedException();
    }
}