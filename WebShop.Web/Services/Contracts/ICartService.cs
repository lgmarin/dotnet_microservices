using WebShop.Web.Models;

namespace WebShop.Web.Services.Contracts;

public class ICartService
{
    Task<CartViewModel> GetCartByUserId(string userId, string token);
    Task<CartViewModel> AddItemToCart(CartViewModel cartVM, string token);
    Task<CartViewModel> UpdateCart(CartViewModel carVM, string token);
    Task<bool> RemoveItemFromCart(int cartId, string token);
}