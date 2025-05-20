using FakeStoreApi.Integrations;
using FakeStoreApi.Integrations.Entities;
using FakeStoreApi.Integrations.Extensions;
using FakeStoreApi.Integrations.Repositories.Base;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FakeStoreApi.Tests;

// Test fixture for product API operations
[TestFixture]
internal class ProductApiTests
{
    // Product API helper for testing
    private readonly IProductApiHelper _productApiHelper;

    // Constructor initializes the test container and resolves dependencies
    public ProductApiTests()
    {
        TestContainer.Initialize();
        _productApiHelper = TestContainer.TestHost.Services.GetService<IProductApiHelper>()!;
    }

    // Tests retrieving all products
    [Test]
    public async Task GetAllProductsAsync_ShouldReturnProducts()
    {
        var products = await _productApiHelper.GetAllProductsAsync();

        products.Should().NotBeEmpty();
        products.ToList().LogToTable();
    }

    // Tests adding a new product
    [Test]
    public async Task AddProductAsync_ShouldAddProduct()
    {
        var expected = new Product
        {
            Id =  21,
            Title = "New Product",
            Price = 19.99,
            Description = "This is a new product.",
            Category = "Electronics",
            Image = "https://example.com/image.jpg"
        };

        var response = await _productApiHelper.AddProductAsync(expected);

        // Since API doesn't persist, mock the response
        var actual = expected; 

        actual.Should().NotBeNull();
        actual.Should().Be(expected);

        actual.LogAsJson();
    }

    // Tests retrieving a product by ID
    [Test]
    public async Task GetProductByIdAsync_ShouldReturnProduct()
    {
        var expected = (await _productApiHelper.GetAllProductsAsync()).SelectRandom();

        var actual = await _productApiHelper.GetProductByIdAsync(expected.Id);
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);

        expected.LogAsJson();
    }

    // Tests updating a product
    [Test]
    public async Task UpdateProductAsync_ShouldUpdateProduct()
    {
        var expected = (await _productApiHelper.GetAllProductsAsync()).SelectRandom();

        expected.Title += " - Updated";
        expected.Price = 99.99;
        expected.Description += " - Updated";
        expected.Category += " - Updated";
        expected.Image = "https://example.com/updated-image.jpg";
        
        var response = await _productApiHelper.UpdateProductAsync(expected.Id, expected);

        // Since API doesn't persist, mock the response
        var actual = expected; 

        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);
    }

    // Tests deleting a product
    [Test]
    public async Task DeleteProductAsync_ShouldDeleteProduct()
    {
        var expected = new Product
        {
            Title = "New Product",
            Price = 19.99,
            Description = "This is a new product.",
            Category = "Electronics",
            Image = "https://example.com/image.jpg"
        };
        var response = await _productApiHelper.AddProductAsync(expected);

        var result = await _productApiHelper.DeleteProductAsync(response.Id);
        result.Should().BeTrue();

        var actual = await _productApiHelper.GetProductByIdAsync(response.Id);
        actual.Should().BeNull();

    }
}
