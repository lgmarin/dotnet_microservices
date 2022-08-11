using System.ComponentModel.DataAnnotations;

namespace WebShop.Web.Models;

public class ProductViewModel
{
    public int Id {get; set;}
    
    [Required]
    public string? Name {get; set;}
    
    [Required]
    public decimal Price {get; set;}
    
    [Required]
    public string? Description {get; set;}
    
    [Required]
    public long Stock {get; set;}
    
    [Required]
    public string? ImageURL {get; set;}
    public string? CategoryName {get; set;}
    
    [Display(Name = "Categories")]
    public int CategoryId {get; set;}

    public int Quantity { get; set; } = 1;
}