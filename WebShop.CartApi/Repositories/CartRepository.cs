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

    public async Task<CartDTO> UpdateCart(CartDTO cartDto)
    {
        var cart = _mapper.Map<Cart>(cartDto);
        
        // Save product in DB if not found
        await SaveProductInDB(cartDto, cart);

        // Verify if the cart header is null
        var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
            c => c.UserId == cart.CartHeader.UserId);
        if (cartHeader is null)
        {
            await CreateHeaderAndItems(cart);
        }
        else
        {
            await UpdateQuantityAndItems(cartDto, cart, cartHeader);
        }

        return _mapper.Map<CartDTO>(cart);

    }

    public async Task<bool> DeleteItemCart(int cartItemId)
    {
        try
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            var totalItems = _context.CartItems.Count(c => c.CartHeaderId == cartItem.CartHeaderId);

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        
            if (totalItems == 1)
            {
                var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartItem.CartHeaderId);

                _context.CartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        
    }
    
    public async Task<bool> CleanCart(string userId)
    {
        var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

        if (cartHeader is not null)
        {
            _context.CartItems.RemoveRange(_context.CartItems.Where(c => c.CartHeaderId == cartHeader.Id));

            _context.CartHeaders.Remove(cartHeader);

            await _context.SaveChangesAsync();
            return true;

        }

        return false;
    }

  
    private async Task SaveProductInDB(CartDTO cartDto, Cart cart)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p =>
            p.Id == cartDto.CartItems.FirstOrDefault().ProductId);

        if (product is null)
        {
            _context.Products.Add(cart.CartItems.FirstOrDefault().Product);
            await _context.SaveChangesAsync();
        }
    }
    
    private async Task CreateHeaderAndItems(Cart cart)
    {
        _context.CartHeaders.Add(cart.CartHeader);
        await _context.SaveChangesAsync();

        cart.CartItems.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
        cart.CartItems.FirstOrDefault().Product = null;

        _context.CartItems.Add(cart.CartItems.FirstOrDefault());

        await _context.SaveChangesAsync();
    }
    
    private async Task UpdateQuantityAndItems(CartDTO cartDto, Cart cart, CartHeader cartHeader)
    {
        var cartDetail = await _context.CartItems.AsNoTracking().FirstOrDefaultAsync(
            p => p.ProductId == cartDto.CartItems.FirstOrDefault()
                .ProductId && p.CartHeaderId == cartHeader.Id);

        if (cartDetail is null)
        {
            cart.CartItems.FirstOrDefault().CartHeaderId = cartHeader.Id;
            cart.CartItems.FirstOrDefault().Product = null;
            _context.CartItems.Add(cart.CartItems.FirstOrDefault());
            await _context.SaveChangesAsync();
        }
        else
        {
            cart.CartItems.FirstOrDefault().Product = null;
            cart.CartItems.FirstOrDefault().Quantity += cartDetail.Quantity;
            cart.CartItems.FirstOrDefault().Id = cartDetail.Id;
            cart.CartItems.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
            _context.CartItems.Update(cart.CartItems.FirstOrDefault());
        }
    }
    
    public async Task<bool> ApplyCoupon(string userId, string couponCode)
    {
        var cartHeaderApply = await _context.CartHeaders.
            FirstOrDefaultAsync(c => c.UserId == userId);

        if (cartHeaderApply is not null)
        {
            cartHeaderApply.CouponCode = couponCode;

            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<bool> DeleteCoupon(string userId)
    {
        var cartHeaderDel = await _context.CartHeaders.
            FirstOrDefaultAsync(c => c.UserId == userId);

        if (cartHeaderDel is not null)
        {
            cartHeaderDel.CouponCode = "";

            _context.CartHeaders.Update(cartHeaderDel);

            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }
}