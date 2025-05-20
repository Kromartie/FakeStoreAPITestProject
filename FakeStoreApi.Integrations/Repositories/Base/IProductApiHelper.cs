using FakeStoreApi.Integrations.Entities;

namespace FakeStoreApi.Integrations.Repositories.Base;

// Interface for product API helper
public interface IProductApiHelper
{
    // Gets all products
    Task<IEnumerable<Product>> GetAllProductsAsync();

    // Gets a product by its ID
    Task<Product> GetProductByIdAsync(int id);

    // Adds a new product
    Task<Product> AddProductAsync(Product product);

    // Updates an existing product
    Task<Product> UpdateProductAsync(int id, Product product);

    // Deletes a product by its ID
    Task<bool> DeleteProductAsync(int id);
}
