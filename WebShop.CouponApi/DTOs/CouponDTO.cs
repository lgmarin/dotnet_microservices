namespace WebShop.CouponApi.DTOs;

public class CouponDTO
{
    public int CouponId { get; set; }
    
    public string? CouponCode { get; set; }
    
    public decimal Discount { get; set; }
}