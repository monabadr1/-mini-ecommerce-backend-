using System.Net.Http.Json;
using Shared;
namespace Front.Consumer
{
    public class ProductConsumer
    {
        private readonly HttpClient _http;

        public ProductConsumer(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _http.GetFromJsonAsync<List<ProductDto>>("api/products");

            return products ?? new List<ProductDto>();
        }

        public async Task<PagedResult<ProductDto>> GetPagedProductsAsync(int page, int pageSize)
        {
            var result = await _http.GetFromJsonAsync<PagedResult<ProductDto>>($"api/products/paged?page={page}&pageSize={pageSize}");
            return result ?? new PagedResult<ProductDto> { Page = page, PageSize = pageSize };
        }
    }
}
