using System.ComponentModel.DataAnnotations;
using WebShop.ProductApi.Models;

namespace WebShop.ProductApi.DTOs;

public class ProductDTO
{
    public int Id {get; set;}
    
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Name {get; set;}
    
    [Required(ErrorMessage = "Price is required")]
    [MinLength(6)]
    [MaxLength(200)]
    public decimal Price {get; set;}
    
    [Required(ErrorMessage = "Description is required")]
    public string? Description {get; set;}
    
    [Required(ErrorMessage = "Stock is required")]
    [Range(1, 9999)]
    public long Stock {get; set;}
    public string? ImageURL {get; set;}

    public string? CategoryName {get; set;}
    //1-n Relationship
    public Category? Category {get; set;}
    public int CategoryId {get; set;}
}