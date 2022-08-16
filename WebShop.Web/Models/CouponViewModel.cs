using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Web.Models;

public class CouponViewModel
{
    public int CouponId { get; set; }

    [Required]
    [StringLength(30)]
    public string? CouponCode { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Discount { get; set; }
}