using FakeStoreApi.Integrations.Entities;

namespace FakeStoreApi.Integrations.Repositories.Base;

// Interface for user API helper
public interface IUserApiHelper
{
    // Gets all users
    Task<IEnumerable<User>> GetAllUsersAsync();

    // Gets a user by their ID
    Task<User> GetUserByIdAsync(int id);

    // Adds a new user
    Task<User> AddUserAsync(User user);

    // Updates an existing user
    Task<User> UpdateUserAsync(int id, User user);

    // Deletes a user by their ID
    Task<bool> DeleteUserAsync(int id);
}
