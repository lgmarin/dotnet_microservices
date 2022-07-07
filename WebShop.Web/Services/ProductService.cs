using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    private const String apiEndpoint = "/api/products/";

    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public async Task<ProductViewModel> FindProductById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteProductById(int id)
    {
        throw new NotImplementedException();
    }
}