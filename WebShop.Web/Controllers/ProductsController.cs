using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Controllers;

public class ProductsController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
}