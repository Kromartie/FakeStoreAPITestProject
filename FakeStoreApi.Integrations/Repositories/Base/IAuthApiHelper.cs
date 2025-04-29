using FakeStoreApi.Integrations.Entities;

namespace FakeStoreApi.Integrations.Repositories.Base;
public interface IAuthApiHelper
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}
