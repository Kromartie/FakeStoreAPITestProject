using FakeStoreApi.Integrations.Configs;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Repositories.Base;
using Microsoft.Extensions.Options;

namespace FakeStoreApi.Integrations.Repositories;

internal class CartApiHelper : BaseApiHttpHelper<Cart>, ICartApiHelper
{
    public CartApiHelper(IOptions<ApiConfig> config) : base($"{config.Value.FakeStoreApiUrl}/carts") { }

    public Task<IEnumerable<Cart>> GetAllCartsAsync() => GetAllAsync();
    public Task<Cart?> GetCartByIdAsync(int id) => GetByIdAsync(id);
    public Task<Cart> AddCartAsync(Cart cart) => PostAsync(cart);
    public Task<Cart> UpdateCartAsync(int id, Cart cart) => PutAsync(id, cart);
    public Task<bool> DeleteCartAsync(int id) => DeleteAsync(id);
}

