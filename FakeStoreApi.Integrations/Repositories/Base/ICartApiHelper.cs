using FakeStoreApi.Integrations.Entities;

namespace FakeStoreApi.Integrations.Repositories.Base;

// Interface for cart API helper
public interface ICartApiHelper
{
    // Gets all carts
    Task<IEnumerable<Cart>> GetAllCartsAsync();

    // Gets a cart by its ID
    Task<Cart> GetCartByIdAsync(int id);

    // Adds a new cart
    Task<Cart> AddCartAsync(Cart cart);

    // Updates an existing cart
    Task<Cart> UpdateCartAsync(int id, Cart cart);

    // Deletes a cart by its ID
    Task<bool> DeleteCartAsync(int id);
}

