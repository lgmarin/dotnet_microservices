using System.Text.Json;
using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;
    
    private const String apiEndpoint = "/api/products/";
    private ProductViewModel _productViewModel;
    private IEnumerable<ProductViewModel> _productsViewModel;

    public ProductService(IHttpClientFactory clientFactory, JsonSerializerOptions options)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
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