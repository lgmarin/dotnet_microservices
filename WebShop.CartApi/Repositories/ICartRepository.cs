using WebShop.CartApi.DTOs;

namespace WebShop.CartApi.Repositories;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserId(string userId);
    Task<CartDTO> UpdateCart(CartDTO cart);
    Task<bool> DeleteItemCart(int cartItemId);
    Task<bool> CleanCart(string userId);
    Task<bool> ApplyCoupon(string userId, string couponCode);
    Task<bool> DeleteCoupon(string userId, string couponCode);
}