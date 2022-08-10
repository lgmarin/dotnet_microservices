using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebShop.CartApi.Context;
using WebShop.CartApi.DTOs;
using WebShop.CartApi.Models;

namespace WebShop.CartApi.Repositories;

public class Cartrepository : ICartRepository
{
    private readonly AppDbContext _context;
    private IMapper _mapper;

    public Cartrepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CartDTO> GetCartByUserId(string userId)
    {
        Cart cart = new Cart()
        {
            CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId)
        };

        cart.CartItems = _context.CartItems.Where(c => c.CartHeaderId == cart.CartHeader.Id)
            .Include(c => c.Product);

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<CartDTO> UpdateCart(CartDTO cart)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteItemCart(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> CleanCart(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ApplyCoupon(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteCoupon(string userId)
    {
        throw new NotImplementedException();
    }
    
}