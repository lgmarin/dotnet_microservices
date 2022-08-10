using Microsoft.Build.Framework;

namespace WebShop.CartApi.DTOs;

public class CartHeaderDTO
{
    public int Id { get; set; }
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;
}