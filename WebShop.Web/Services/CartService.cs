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
    private CartHeaderViewModel _cartHeaderViewModel = new CartHeaderViewModel();

    public CartService(IHttpClientFactory clientFactory)
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

    public async Task<CartViewModel> UpdateCart(CartViewModel cartVM, string token)
    {
        var client = _clientFactory.CreateClient("CartApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var content = new StringContent(JsonSerializer.Serialize(cartVM), 
            Encoding.UTF8, "application/json");

        CartViewModel updatedCartViewModel = new();

        using (var response = await client.PutAsync($"{ApiEndPoint}/addcart/", content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                updatedCartViewModel = await JsonSerializer.DeserializeAsync<CartViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return updatedCartViewModel;
    }

    public async Task<bool> RemoveItemFromCart(int cartId, string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        using (var response = await client.DeleteAsync($"{ApiEndPoint}/deletecart/{cartId}"))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return true;
        }
    }

    public async Task<bool> ClearCart(string userId, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ApplyCoupon(CartViewModel cartVM, string token)
    {
        var client = _clientFactory.CreateClient("CartApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var content = new StringContent(JsonSerializer.Serialize(cartVM), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync($"{ApiEndPoint}/applycoupon", content))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }

    public async Task<bool> RemoveCoupon(string userId, string token)
    {
        var client = _clientFactory.CreateClient("CartApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        using (var response = await client.DeleteAsync($"{ApiEndPoint}/deletecoupon/{userId}"))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }

    public async Task<CartHeaderViewModel> Checkout(CartHeaderViewModel cartHeader, string token)
    {
        var client = _clientFactory.CreateClient("CartApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var content = new StringContent(JsonSerializer.Serialize(cartHeader), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync($"{ApiEndPoint}/checkout/", content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                cartHeader = await JsonSerializer.DeserializeAsync<CartHeaderViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return cartHeader;
    }
}