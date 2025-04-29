using FakeStoreApi.Integrations.Entities;

namespace FakeStoreApi.Integrations.Repositories.Base;
public interface ICartApiHelper
{
    Task<IEnumerable<Cart>> GetAllCartsAsync();
    Task<Cart?> GetCartByIdAsync(int id);
    Task<Cart> AddCartAsync(Cart newCart);
    Task<Cart> UpdateCartAsync(int id, Cart updatedCart);
    Task<bool> DeleteCartAsync(int id);
}

