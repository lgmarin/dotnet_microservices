namespace WebShop.Web.Models;

public class CartViewModel
{
    public CarHeaderViewModel CartHeader { get; set; } = new CarHeaderViewModel();
    public IEnumerable<CartItemViewModel> CartItems { get; set; } = Enumerable.Empty<CartItemViewModel>();
}