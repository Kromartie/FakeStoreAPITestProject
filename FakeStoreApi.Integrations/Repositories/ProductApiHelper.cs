using FakeStoreApi.Integrations.Configs;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Repositories.Base;
using Microsoft.Extensions.Options;

namespace FakeStoreApi.Integrations.Repositories;

// Helper for product API operations
internal class ProductApiHelper : BaseApiHttpHelper<Product>, IProductApiHelper
{
    // Constructor sets up the base URL for product API endpoints
    public ProductApiHelper(HttpClient httpClient, IOptions<ApiConfig> config) 
        : base(httpClient, $"{config.Value.FakeStoreApiUrl}/products") { }

    // Gets all products
    public Task<IEnumerable<Product>> GetAllProductsAsync() => GetAllAsync();
    // Gets a product by its ID
    public Task<Product?> GetProductByIdAsync(int id) => GetByIdAsync(id);
    // Adds a new product
    public Task<Product> AddProductAsync(Product product) => PostAsync(product);
    // Updates an existing product
    public Task<Product> UpdateProductAsync(int id, Product product) => PutAsync(id, product);
    // Deletes a product by its ID
    public Task<bool> DeleteProductAsync(int id) => DeleteAsync(id);
}
