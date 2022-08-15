using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebShop.CouponApi.Models;

public class Coupon
{
    public int CouponId { get; set; }

    [Required]
    [StringLength(30)]
    public string? CouponCode { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Discount { get; set; }
}