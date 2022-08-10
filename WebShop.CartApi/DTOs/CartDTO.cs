using WebShop.CartApi.Models;

namespace WebShop.CartApi.DTOs;

public class CartDTO
{
    public CartHeader CartHeader { get; set; } = new CartHeader();
    public IEnumerable<CartItem> CartItems { get; set; } = Enumerable.Empty<CartItem>();
}