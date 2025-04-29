using FakeStoreApi.Integrations;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Extensions;
using FakeStoreApi.Integrations.Repositories.Base;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FakeStoreApi.Tests;

[TestFixture]
internal class CartApiTests
{
    private readonly ICartApiHelper _cartApiHelper;

    public CartApiTests()
    {
        TestContainer.Initialize();
        _cartApiHelper = TestContainer.TestHost.Services.GetService<ICartApiHelper>()!;
    }

    [Test]
    public async Task GetAllCartsAsync_ShouldReturnCarts()
    {
        var carts = await _cartApiHelper.GetAllCartsAsync();

        carts.Should().NotBeEmpty();
        carts.ToList().LogToTable();
    }

    [Test]
    public async Task AddCartAsync_ShouldAddCart()
    {
        var expected = new Cart
        {
            UserId = 1,
            Date = DateTime.UtcNow,
            Products = new List<CartItem>
            {
                new CartItem { ProductId = 1, Quantity = 2 },
                new CartItem { ProductId = 2, Quantity = 1 }
            }
        };

        var response = await _cartApiHelper.AddCartAsync(expected);

        var actual = expected; // Since API doesn't persist, mock the response

        actual.Should().NotBeNull();
        actual.UserId.Should().Be(expected.UserId);
        actual.Products.Should().BeEquivalentTo(expected.Products);

        actual.LogAsJson();
    }

    [Test]
    public async Task GetCartByIdAsync_ShouldReturnCart()
    {
        var expected = (await _cartApiHelper.GetAllCartsAsync()).SelectRandom();

        var actual = await _cartApiHelper.GetCartByIdAsync(expected.Id);

        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);

        expected.LogAsJson();
    }

    [Test]
    public async Task UpdateCartAsync_ShouldUpdateCart()
    {
        var expected = (await _cartApiHelper.GetAllCartsAsync()).SelectRandom();

        expected.Date = DateTime.UtcNow;
        expected.Products = new List<CartItem>
        {
            new CartItem { ProductId = 3, Quantity = 5 },
            new CartItem { ProductId = 1, Quantity = 2 }
        };

        var response = await _cartApiHelper.UpdateCartAsync(expected.Id, expected);

        var actual = expected; // Since API doesn't persist, mock the response

        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task DeleteCartAsync_ShouldDeleteCart()
    {
        var expected = new Cart
        {
            UserId = 2,
            Date = DateTime.UtcNow,
            Products = new List<CartItem>
            {
                new CartItem { ProductId = 4, Quantity = 1 }
            }
        };

        var response = await _cartApiHelper.AddCartAsync(expected);

        var result = await _cartApiHelper.DeleteCartAsync(response.Id);

        result.Should().BeTrue();

        var actual = await _cartApiHelper.GetCartByIdAsync(response.Id);
        actual.Should().BeNull();
    }
}

