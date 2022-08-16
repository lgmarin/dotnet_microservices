using WebShop.Web.Models;

namespace WebShop.Web.Services.Contracts;

public interface ICartService
{
    Task<CartViewModel> GetCartByUserId(string userId, string token);
    Task<CartViewModel> AddItemToCart(CartViewModel cartVM, string token);
    Task<CartViewModel> UpdateCart(CartViewModel cartVM, string token);
    Task<bool> RemoveItemFromCart(int cartId, string token);
    Task<bool> ClearCart(string userId, string token);
    Task<bool> ApplyCoupon(CartViewModel cartVM, string token);
    Task<bool> RemoveCoupon(string userId, string token);
    Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string token);
}