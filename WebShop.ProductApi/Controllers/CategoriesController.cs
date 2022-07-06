using Microsoft.AspNetCore.Mvc;
using WebShop.ProductApi.DTOs;
using WebShop.ProductApi.Services;

namespace WebShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categoriesDto = await _categoryService.GetCategories();
        if (categoriesDto is null)
        {
            return NotFound("No categories found.");
        }
        return Ok(categoriesDto);
    }
    
    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetcategoriesProducts()
    {
        var categoriesDto = await _categoryService.GetCategoriesProducts();
        if (categoriesDto is null)
        {
            return NotFound("No categories found.");
        }
        return Ok(categoriesDto);
    }

    [HttpGet("{id}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var categoryDto = await _categoryService.GetCategoryById(id);
        if (categoryDto is null)
        {
            return NotFound("Category not found");
        }

        return Ok(categoryDto);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
    {
        if (categoryDto is null)
        {
            return BadRequest("Invalid data.");
        }

        await _categoryService.AddCategory(categoryDto);

        return new CreatedAtRouteResult("GetCategory", new { id = categoryDto.CategoryId }, categoryDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
    {
        if (id != categoryDto.CategoryId)
            return BadRequest();

        if (categoryDto is null)
            return BadRequest();

        await _categoryService.UpdateCategory(categoryDto);

        return Ok(categoryDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<CategoryDTO>> Delete(int id)
    {
        var categoryDto = await _categoryService.GetCategoryById(id);

        if (categoryDto is null)
            return NotFound("Category not found!");

        await _categoryService.RemoveCategory(id);

        return Ok(categoryDto);
    }    
}

