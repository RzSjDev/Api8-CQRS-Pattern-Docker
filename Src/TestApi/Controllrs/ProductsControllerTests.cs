using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Src.api_net8.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Src.api_net8.Application.ProductFeature.Command;

namespace Src.TestApi.Controllrs
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductsControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostProduct_ShouldCreateNewProduct()
        {
            // Arrange
            var newProduct = new Product
            {
                ChangeDate = DateTime.Now,
                ProductCode = "4",
                ProductName = "Test",
                Unit = "4"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Product", newProduct);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdProduct = await response.Content.ReadFromJsonAsync<AddProductCommand>();
            createdProduct.Should().NotBeNull();
        }
        [Fact]
        public async Task GetAllProducts_ReturnsOk()
        {
            // Act
            var response = await _client.GetAsync("/api/Product");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetProductById_ShouldReturnOk_WhenProductExists()
        {
            // Arrange
            int existingProductId = 1;

            // Act
            var response = await _client.GetAsync($"/api/Product/{existingProductId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            int nonExistingProductId = 99999;

            // Act
            var response = await _client.GetAsync($"/api/Product/{nonExistingProductId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
