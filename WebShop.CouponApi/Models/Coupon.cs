using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebShop.CouponApi.Models;

public class Coupon
{
    public int CouponId { get; set; }

    [Required]
    [StringLength(30)]
    public string? CouponCode { get; set; }
    
    [Required]
    public decimal Discount { get; set; }
}