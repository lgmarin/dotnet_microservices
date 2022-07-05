using WebShop.ProductApi.Models;

namespace WebShop.ProductApi.DTOs;

public class CategoryDTO
{
    public int CategoryId {get; set;}
    public string? Name {get; set;}
    public ICollection<Product>? Products {get; set;}
}