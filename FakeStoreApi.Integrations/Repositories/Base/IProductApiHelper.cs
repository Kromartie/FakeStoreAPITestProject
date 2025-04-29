using FakeStoreApi.Integrations.Entities;

namespace FakeStoreApi.Integrations.Repositories.Base;
public interface IProductApiHelper
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> AddProductAsync(Product newProduct);
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> UpdateProductAsync(int id, Product updatedProduct);
    Task<bool> DeleteProductAsync(int id);
}
