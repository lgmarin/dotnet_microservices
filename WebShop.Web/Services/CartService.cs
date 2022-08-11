using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class CartService : ICartService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions? _options;
    private const string ApiEndPoint = "/api/cart";
    private CartViewModel _cartViewModel = new CartViewModel();

    public CartService(IHttpClientFactory clientFactory, JsonSerializerOptions? options)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
    }

    public async Task<CartViewModel> GetCartByUserId(string userId, string token)
    {
        var client = _clientFactory.CreateClient("CartApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        using (var response = await client.GetAsync($"{ApiEndPoint}/getcart/{userId}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                _cartViewModel = await JsonSerializer.DeserializeAsync<CartViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return _cartViewModel;
    }

    public async Task<CartViewModel> AddItemToCart(CartViewModel cartVM, string token)
    {
        var client = _clientFactory.CreateClient("CartApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var content = new StringContent(JsonSerializer.Serialize(cartVM), 
            Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync($"{ApiEndPoint}/addcart/", content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                _cartViewModel = await JsonSerializer.DeserializeAsync<CartViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return _cartViewModel;
    }

    public async Task<CartViewModel> UpdateCart(CartViewModel carVM, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveItemFromCart(int cartId, string token)
    {
        throw new NotImplementedException();
    }
}