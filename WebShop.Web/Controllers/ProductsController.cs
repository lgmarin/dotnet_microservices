using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.Web.Models;
using WebShop.Web.Roles;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Controllers;

[Authorize(Roles = Role.Admin)]
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
        var result = await _productService.GetAllProducts(await GetAccessToken());

        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await
            _categoryService.GetAllCategories(await GetAccessToken()), "CategoryId", "Name");

        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProduct(productViewModel, await GetAccessToken());

            if (result is not null)
                return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewBag.CategoryId = new SelectList(await
                _categoryService.GetAllCategories(await GetAccessToken()), "CategoryId", "Name");
        }
        return View(productViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        ViewBag.CategoryId = new SelectList(await 
            _categoryService.GetAllCategories(await GetAccessToken()), "CategoryId", "Name");

        var result = await _productService.FindProductById(id, await GetAccessToken());

        if (result is null)
        {
            return View("Error");
        }

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateProduct(productViewModel, await GetAccessToken());

            if (result is null)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return View(productViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productService.FindProductById(id, await GetAccessToken());
 
        if (result is null)
        {
            return View("Error");
        }

        return View(result);
    }
    
    [HttpPost(), ActionName("DeleteProduct")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _productService.DeleteProductById(id, await GetAccessToken());
 
        if (!result)
        {
            return View("Error");
        }
        return RedirectToAction("Index");
    }

    private async Task<String> GetAccessToken()
    {
        return await HttpContext.GetTokenAsync("access-token");
    }
}