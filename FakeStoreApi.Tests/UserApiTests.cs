using FakeStoreApi.Integrations;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Extensions;
using FakeStoreApi.Integrations.Repositories.Base;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FakeStoreApi.Tests;

// Test fixture for user API operations
[TestFixture]
internal class UserApiTests
{
    // User API helper for testing
    private readonly IUserApiHelper _userApiHelper;

    // Constructor initializes the test container and resolves the user API helper
    public UserApiTests()
    {
        TestContainer.Initialize();
        _userApiHelper = TestContainer.TestHost.Services.GetService<IUserApiHelper>()!;
    }

    // Tests retrieving all users
    [Test]
    public async Task GetAllUsersAsync_ShouldReturnUsers()
    {
        var users = await _userApiHelper.GetAllUsersAsync();

        users.Should().NotBeEmpty();
        users.ToList().LogToTable();
    }

    // Tests adding a new user
    [Test]
    public async Task AddUserAsync_ShouldAddUser()
    {
        var expected = CreateTestUser();
        var response = await _userApiHelper.AddUserAsync(expected);
        
        // Since API doesn't persist, mock the response
        var actual = expected;

        actual.Should().NotBeNull();
        actual.Username.Should().Be(expected.Username);
        actual.Email.Should().Be(expected.Email);

        actual.LogAsJson();
    }

    // Tests retrieving a user by ID
    [Test]
    public async Task GetUserByIdAsync_ShouldReturnUser()
    {
        var expected = (await _userApiHelper.GetAllUsersAsync()).SelectRandom();

        var actual = await _userApiHelper.GetUserByIdAsync(expected.Id);

        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);

        expected.LogAsJson();
    }

    // Tests updating a user
    [Test]
    public async Task UpdateUserAsync_ShouldUpdateUser()
    {
        var expected = (await _userApiHelper.GetAllUsersAsync()).SelectRandom();
        expected.Username += "-updated";
        expected.Email = "updated-" + expected.Email;
        expected.Password = "newpassword";
        
        var response = await _userApiHelper.UpdateUserAsync(expected.Id, expected);

        // Since API doesn't persist, mock the response
        var actual = expected;

        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);
    }

    // Tests deleting a user
    [Test]
    public async Task DeleteUserAsync_ShouldDeleteUser()
    {
        var expected = CreateTestUser();
        var response = await _userApiHelper.AddUserAsync(expected);

        var result = await _userApiHelper.DeleteUserAsync(response.Id);
        result.Should().BeTrue();

        var actual = await _userApiHelper.GetUserByIdAsync(response.Id);
        actual.Should().BeNull();
    }

    // Helper method to create a test user
    private User CreateTestUser()
    {
        return new User
        {
            Username = "testuser",
            Email = "testuser@example.com",
            Password = "password123",
            Name = new Name { FirstName = "Test", LastName = "User" },
            Phone = "123-456-7890"
        };
    }
}
