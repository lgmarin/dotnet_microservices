using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class CartService : ICartService
{
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