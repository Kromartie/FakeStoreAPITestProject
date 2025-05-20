using FakeStoreApi.Integrations.Configs;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Repositories.Base;
using Microsoft.Extensions.Options;

namespace FakeStoreApi.Integrations.Repositories;

// Helper for cart API operations
internal class CartApiHelper : BaseApiHttpHelper<Cart>, ICartApiHelper
{
    // Constructor sets up HttpClient and base URL from config
    public CartApiHelper(HttpClient httpClient, IOptions<ApiConfig> config) 
        : base(httpClient, $"{config.Value.FakeStoreApiUrl}/carts") { }

    // Gets all carts
    public Task<IEnumerable<Cart>> GetAllCartsAsync() => GetAllAsync();
    // Gets a cart by its ID
    public Task<Cart?> GetCartByIdAsync(int id) => GetByIdAsync(id);
    // Adds a new cart
    public Task<Cart> AddCartAsync(Cart cart) => PostAsync(cart);
    // Updates an existing cart
    public Task<Cart> UpdateCartAsync(int id, Cart cart) => PutAsync(id, cart);
    // Deletes a cart by its ID
    public Task<bool> DeleteCartAsync(int id) => DeleteAsync(id);
}

