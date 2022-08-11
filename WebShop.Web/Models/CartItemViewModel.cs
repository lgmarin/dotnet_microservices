namespace WebShop.Web.Models;

public class CartItemViewModel
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int CartHeaderId { get; set; }
    public ProductViewModel Product { get; set; } = new ProductViewModel();
    public CartHeaderViewModel CartHeader { get; set; } = new CartHeaderViewModel();
}