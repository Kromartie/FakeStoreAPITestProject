using FakeStoreApi.Integrations.Entities;

namespace FakeStoreApi.Integrations.Repositories.Base;

// Interface for authentication API helper
public interface IAuthApiHelper
{
    // Authenticates a user and returns a login response
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
