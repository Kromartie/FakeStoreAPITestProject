using FakeStoreApi.Integrations.Configs;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Repositories.Base;
using Microsoft.Extensions.Options;

namespace FakeStoreApi.Integrations.Repositories;

// Helper for user API operations
internal class UserApiHelper : BaseApiHttpHelper<User>, IUserApiHelper
{
    // Constructor sets up the base URL for user API endpoints
    public UserApiHelper(HttpClient httpClient, IOptions<ApiConfig> config) 
        : base(httpClient, $"{config.Value.FakeStoreApiUrl}/users") { }

    // Gets all users
    public Task<IEnumerable<User>> GetAllUsersAsync() => GetAllAsync();
    // Gets a user by their ID
    public Task<User?> GetUserByIdAsync(int id) => GetByIdAsync(id);
    // Adds a new user
    public Task<User> AddUserAsync(User user) => PostAsync(user);
    // Updates an existing user
    public Task<User> UpdateUserAsync(int id, User user) => PutAsync(id, user);
    // Deletes a user by their ID
    public Task<bool> DeleteUserAsync(int id) => DeleteAsync(id);
}
