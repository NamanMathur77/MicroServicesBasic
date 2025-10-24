using System.Net.Http.Json;

namespace OrderService.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductApiService> _logger;

        public ProductApiService(HttpClient httpClient, ILogger<ProductApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _httpClient.GetFromJsonAsync<Product>($"api/product/{id}");
                return product;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error calling Product Service");
                return null;
            }
        }

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public double Price { get; set; }
        }
    }
}