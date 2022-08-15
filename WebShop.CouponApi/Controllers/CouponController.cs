using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.CouponApi.DTOs;
using WebShop.CouponApi.Repositories;

namespace WebShop.CouponApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private ICouponRepository _repository;

    public CouponController(ICouponRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{couponCode}")]
    [Authorize]
    public async Task<ActionResult<CouponDTO>> GetDiscountByCode(string couponCode)
    {
        var coupon = await _repository.GetCouponByCode(couponCode);

        if (coupon is null)
        {
            return NotFound($"Coupon code: {couponCode} not found!");
        }

        return Ok(coupon);
    }
}