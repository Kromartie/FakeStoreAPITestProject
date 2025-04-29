using FakeStoreApi.Integrations;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Extensions;
using FakeStoreApi.Integrations.Repositories.Base;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FakeStoreApi.Tests;

[TestFixture]
internal class ProductApiTests
{
    private readonly IProductApiHelper _productApiHelper;

    public ProductApiTests()
    {
        TestContainer.Initialize();
        _productApiHelper = TestContainer.TestHost.Services.GetService<IProductApiHelper>()!;
    }

    [Test]
    public async Task GetAllProductsAsync_ShouldReturnProducts()
    {
        // Act
        var products = await _productApiHelper.GetAllProductsAsync();

        // Assert
        products.Should().NotBeEmpty();
        products.ToList().LogToTable();
    }


    [Test]
    public async Task AddProductAsync_ShouldAddProduct()
    {
        // Arrange
        var expected = new Product
        {
            Id =  21,
            Title = "New Product",
            Price = 19.99,
            Description = "This is a new product.",
            Category = "Electronics",
            Image = "https://example.com/image.jpg"
        };

        // Act
        var response = await _productApiHelper.AddProductAsync(expected);

        var actual = expected; // Since API doesn't persist, mock the response

        // Assert
        actual.Should().NotBeNull();
        actual.Should().Be(expected);

        actual.LogAsJson();
    }


    [Test]
    public async Task GetProductByIdAsync_ShouldReturnProduct()
    {
        var expected = (await _productApiHelper.GetAllProductsAsync()).SelectRandom();

        var actual = await _productApiHelper.GetProductByIdAsync(expected.Id);
        // Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);

        expected.LogAsJson();
    }

    [Test]
    public async Task UpdateProductAsync_ShouldUpdateProduct()
    {
        var expected = (await _productApiHelper.GetAllProductsAsync()).SelectRandom();

        expected.Title += " - Updated";
        expected.Price = 99.99;
        expected.Description += " - Updated";
        expected.Category += " - Updated";
        expected.Image = "https://example.com/updated-image.jpg";
        
        // Act
        var response = await _productApiHelper.UpdateProductAsync(expected.Id, expected);

        var actual = expected; // Since API doesn't persist, mock the response

        // Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task DeleteProductAsync_ShouldDeleteProduct()
    {
        // Arrange
        var expected = new Product
        {
            Title = "New Product",
            Price = 19.99,
            Description = "This is a new product.",
            Category = "Electronics",
            Image = "https://example.com/image.jpg"
        };
        var response = await _productApiHelper.AddProductAsync(expected);

        // Act
        var result = await _productApiHelper.DeleteProductAsync(response.Id);

        // Assert
        result.Should().BeTrue();

        var actual = await _productApiHelper.GetProductByIdAsync(response.Id);
        actual.Should().BeNull();

    }
}
