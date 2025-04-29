using FakeStoreApi.Integrations.Configs;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Repositories.Base;
using Microsoft.Extensions.Options;

namespace FakeStoreApi.Integrations.Repositories;
internal class UserApiHelper : BaseApiHttpHelper<User>, IUserApiHelper
{
    public UserApiHelper(IOptions<ApiConfig> config) : base($"{config.Value.FakeStoreApiUrl}/users") { }

    public Task<IEnumerable<User>> GetAllUsersAsync() => GetAllAsync();
    public Task<User?> GetUserByIdAsync(int id) => GetByIdAsync(id);
    public Task<User> AddUserAsync(User user) => PostAsync(user);
    public Task<User> UpdateUserAsync(int id, User user) => PutAsync(id, user);
    public Task<bool> DeleteUserAsync(int id) => DeleteAsync(id);
}
