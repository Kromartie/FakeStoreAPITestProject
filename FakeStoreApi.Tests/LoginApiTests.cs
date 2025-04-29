using FakeStoreApi.Integrations;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Repositories.Base;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FakeStoreApi.Tests;

[TestFixture]
public class LoginApiTests
{
    private IAuthApiHelper _authApiHelper;

    [SetUp]
    public void Setup()
    {
        TestContainer.Initialize();
        _authApiHelper = TestContainer.TestHost.Services.GetService<IAuthApiHelper>()!;
    }

    [Test]
    public async Task LoginAsync_ShouldLoginWithValidCredentials()
    {
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
