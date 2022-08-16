using Microsoft.AspNetCore.Mvc;
using WebShop.Web.Models;

namespace WebShop.Web.Services.Contracts;

public interface ICouponService
{
    Task<CouponViewModel> GetCouponByCode(string couponCode);
}