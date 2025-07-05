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
using Src.api_net8.Application.ProductFeature.Command;
using Src.api_net8.Domain.Models;
using Src.api_net8.Application.FactorFeature.Command.AddCommand;
using Src.api_net8.Application.FactorDetailFeature.Command.AddCommand;

namespace Src.TestApi.Controllrs
{
    public class FactorDetailControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public FactorDetailControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostFactorDetail_ShouldCreateNewFactorDetail()
        {
            // Arrange
            var newFactorDetail = new FactorDetail
            {
                FactorId = 1,
                ProductId = 1,
                Count = 1,
                ProductDescription = "Test",
                UnitPrice = 3000,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/FactorDetail", newFactorDetail);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdProduct = await response.Content.ReadFromJsonAsync<AddFactorDetailCommand>();
            createdProduct.Should().NotBeNull();
        }
        [Fact]
        public async Task UpdateFactorDetail_ReturnsOk()
        {
            // Arrange
            var UpdateFactorDetail = new FactorDetail
            {
                FactorDetailId = 1,
                FactorId = 1,
                ProductId = 1,
                Count = 1,
                ProductDescription = "Test",
                UnitPrice = 3000,
            };
            // Act
            var response = await _client.PutAsJsonAsync("/api/FactorDetail", UpdateFactorDetail);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
