using WebShop.CartApi.DTOs;

namespace WebShop.CartApi.Repositories;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserId(string userId);
    Task<CartDTO> UpdateCart(CartDTO cart);
    Task<bool> DeleteItemCart(string userId, string couponCode);
    Task<bool> ApplyCoupon(string userId);
    Task<bool> DeleteCoupon(string userId);
    Task<bool> CleanCart(string userId);
}