using Microsoft.AspNetCore.Mvc;
using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var result = await _productService.GetAllProducts();

        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await 
            _categoryService)
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