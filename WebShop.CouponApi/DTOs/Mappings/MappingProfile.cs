using AutoMapper;
using WebShop.CouponApi.Models;

namespace WebShop.CouponApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CouponDTO, Coupon>().ReverseMap();
    }
}