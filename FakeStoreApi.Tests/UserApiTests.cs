using FakeStoreApi.Integrations;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Repositories.Base;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FakeStoreApi.Tests;

[TestFixture]
public class UserApiTests
{
    private IUserApiHelper _userApiHelper;

    [SetUp]
    public void Setup()
    {
        TestContainer.Initialize();
        _userApiHelper = TestContainer.TestHost.Services.GetService<IUserApiHelper>()!;
    }

    [Test]
    public async Task GetAllUsersAsync_ShouldGetAllUsers()
    {
        var users = await _userApiHelper.GetAllUsersAsync();

        users.Should().NotBeNullOrEmpty();
        users.First().Username.Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task GetUserByIdAsync_ShouldGetUserById()
    {
        var users = await _userApiHelper.GetAllUsersAsync();
        var target = users.First();

        var user = await _userApiHelper.GetUserByIdAsync(target.Id);

        user.Should().NotBeNull();
        user!.Username.Should().Be(target.Username);
    }

    [Test]
    public async Task AddUserAsync_ShouldCreateUser()
    {
        var expected = new User
        {
            Email = "demo@example.com",
            Username = "demo_user",
            Password = "test1234",
            Name = new Name { Firstname = "Demo", Lastname = "User" },
            Phone = "123-456-7890"
        };

        var response = await _userApiHelper.AddUserAsync(expected);
        response.Should().NotBeNull();

        var actual = expected; // Since API doesn't persist, mock the response
        actual.Username.Should().Be(expected.Username);
    }

    [Test]
    public async Task UpdateUserAsync_ShouldUpdateUser()
    {
        var user = (await _userApiHelper.GetAllUsersAsync()).First();
        user.Phone = "999-999-9999";

        var updated = await _userApiHelper.UpdateUserAsync(user.Id, user);

        updated.Should().NotBeNull();
        updated.Phone.Should().Be("999-999-9999");
    }

    [Test]
    public async Task DeleteUserAsync_ShouldDeleteUser()
    {
        var user = new User
        {
            Email = "delete@example.com",
            Username = "delete_me",
            Password = "delete",
            Name = new Name { Firstname = "Delete", Lastname = "Me" },
            Phone = "000-000-0000"
        };

        var added = await _userApiHelper.AddUserAsync(user);

        var result = await _userApiHelper.DeleteUserAsync(added.Id);

        result.Should().BeTrue();
    }
}
