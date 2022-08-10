using WebShop.CartApi.DTOs;

namespace WebShop.CartApi.Repositories;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserIdAsync(string userId);
    Task<CartDTO> UpdateCartAsync(CartDTO cart);
    Task<bool> DeleteItemCartAsync(string userId, string couponCode);
    Task<bool> ApplyCouponAsync(string userId);
    Task<bool> DeleteCouponAsync(string userId);
    Task<bool> CleanCartAsync(string userId);
}