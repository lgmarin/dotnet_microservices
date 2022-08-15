using Microsoft.EntityFrameworkCore;
using WebShop.CouponApi.DTOs;
using WebShop.CouponApi.Models;

namespace WebShop.CouponApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    private DbSet<Coupon>? Discounts { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        mb.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = 1,
            CouponCode = "PROMO10",
            Discount = 10
        });

        mb.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = 1,
            CouponCode = "PROMO20",
            Discount = 20
        });

    }
}