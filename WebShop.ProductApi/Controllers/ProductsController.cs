using Microsoft.AspNetCore.Mvc;
using WebShop.ProductApi.DTOs;
using WebShop.ProductApi.Services;

namespace WebShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;


    public ProductsController(IProductService categoryService)
    {
        _productService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var productsDto = await _productService.GetProducts();
        if (productsDto is null)
        {
            return NotFound("No categories found.");
        }
        return Ok(productsDto);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var productDto = await _productService.GetProductById(id);
        if (productDto is null)
        {
            return NotFound("Category not found");
        }

        return Ok(productDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
    {
        if (productDto is null)
        {
            return BadRequest("Invalid data.");
        }

        await _productService.AddProduct(productDto);

        return new CreatedAtRouteResult("GetProduct",
            new { id = productDto.CategoryId }, productDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDto)
    {
        if (id != productDto.CategoryId)
            return BadRequest();

        if (productDto is null)
            return BadRequest();

        await _productService.UpdateProduct(productDto);

        return Ok(productDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductDTO>> Delete(int id)
    {
        var productDto = await _productService.GetProductById(id);
        
        if (productDto is null)
            return NotFound("Product not found!");

        await _productService.RemoveProduct(id);

        return Ok(productDto);
    }
}

