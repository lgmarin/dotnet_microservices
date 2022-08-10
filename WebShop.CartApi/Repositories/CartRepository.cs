using WebShop.CartApi.DTOs;

namespace WebShop.CartApi.Repositories;

public class Cartrepository : ICartRepository
{
    public async Task<CartDTO> GetCartByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<CartDTO> UpdateCartAsync(CartDTO cart)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteItemCartAsync(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ApplyCouponAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteCouponAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CleanCartAsync(string userId)
    {
        throw new NotImplementedException();
    }
}