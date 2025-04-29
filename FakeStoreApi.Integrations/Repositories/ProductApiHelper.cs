using FakeStoreApi.Integrations.Configs;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Repositories.Base;
using Microsoft.Extensions.Options;

namespace FakeStoreApi.Integrations.Repositories;
internal class ProductApiHelper : BaseApiHttpHelper<Product>, IProductApiHelper
{
    public ProductApiHelper(IOptions<ApiConfig> config) : base($"{config.Value.FakeStoreApiUrl}/products") { }

    public Task<IEnumerable<Product>> GetAllProductsAsync() => GetAllAsync();
    public Task<Product?> GetProductByIdAsync(int id) => GetByIdAsync(id);
    public Task<Product> AddProductAsync(Product product) => PostAsync(product);
    public Task<Product> UpdateProductAsync(int id, Product product) => PutAsync(id, product);
    public Task<bool> DeleteProductAsync(int id) => DeleteAsync(id);
}
