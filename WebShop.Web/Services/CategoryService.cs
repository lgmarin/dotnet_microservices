using System.Text.Json;
using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class CategoryService : ICategoryService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;
    
    private IEnumerable<CategoryViewModel> _categoriesViewModel;
    private const string apiEndpoint = "/api/categories/";

    public CategoryService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                _categoriesViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return _categoriesViewModel;
    }
}