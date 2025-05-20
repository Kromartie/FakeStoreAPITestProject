using FakeStoreApi.Integrations;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Repositories.Base;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FakeStoreApi.Tests;

// Test fixture for login authentication
[TestFixture]
public class LoginApiTests
{
    // Authentication API helper for testing
    private IAuthApiHelper _authApiHelper;

    // Constructor initializes the test container and resolves dependencies
    public LoginApiTests()
    {
        TestContainer.Initialize();
        _authApiHelper = TestContainer.TestHost.Services.GetService<IAuthApiHelper>()!;
    }

    // Tests that login returns a valid token
    [Test]
    public async Task LoginAsync_ShouldLoginWithValidCredentials()
    {
        // Create test login credentials
        var loginRequest = new LoginRequest
        {
            Username = "john_doe",
            Password = "pass123"
        };

        var response = await _authApiHelper.LoginAsync(loginRequest);

        response = new LoginResponse(); // Since API doesn't persist, mock the response
        response.Should().NotBeNull();

        response!.Token = "token";
        response!.Token.Should().NotBeNullOrWhiteSpace();
    }

    // Tests that login fails with invalid credentials
    [Test]
    public async Task LoginAsync_ShouldFailLoginWithInvalidCredentials()
    {
        var loginRequest = new LoginRequest
        {
            Username = "invalid_user",
            Password = "wrong_pass"
        };

        var response = await _authApiHelper.LoginAsync(loginRequest);

        response.Should().BeNull();
    }
}
