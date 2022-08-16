using System.Net.Http.Headers;
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

    public async Task<CouponViewModel> GetCouponByCode(string couponCode, string token)
    {
        var client = _clientFactory.CreateClient("CouponApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        using (var response = await client.GetAsync($"{apiEndpoint}/{couponCode}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                _couponViewModel = await JsonSerializer.DeserializeAsync<CouponViewModel>(apiResponse, _options);

                return _couponViewModel;
            }
        }

        return null;
    }
}