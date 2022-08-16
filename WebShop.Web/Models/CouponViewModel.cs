using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Web.Models;

public class CouponViewModel
{
    public int CouponId { get; set; }
    public string? CouponCode { get; set; }
    public decimal Discount { get; set; }
}