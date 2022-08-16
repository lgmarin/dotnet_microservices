using System.Text.Json;
using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class CouponService : ICouponService
{
    private readonly IHttpClientFactory _clientFactory;
    private JsonSerializerOptions _options;

    private const string apiEndpoint = "/api/coupon";
    private CouponViewModel _couponViewModel;


    public CouponService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
    }

    public async Task<CouponViewModel> GetCouponByCode(string couponCode)
    {
        throw new NotImplementedException();
    }
}