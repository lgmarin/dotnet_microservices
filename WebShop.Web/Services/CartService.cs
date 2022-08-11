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
        throw new NotImplementedException();
    }

    public async Task<CartViewModel> AddItemToCart(CartViewModel cartVM, string token)
    {
        throw new NotImplementedException();
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