using WebShop.CouponApi.DTOs;

namespace WebShop.CouponApi.Repositories;

public interface ICouponRepository
{
    Task<CouponDTO> GetCouponByCode(string couponCode);
}