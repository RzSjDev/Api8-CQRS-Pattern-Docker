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
using Newtonsoft.Json;
using api_net9.Domain.Models;
using api_net9.Application.FactorFeature.Command.AddCommand;

namespace Src.TestApi.Controllrs
{
    public class FactorControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public FactorControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostFactor_ShouldCreateNewFactor()
        {
            // Arrange
            var newFactor = new Factor
            {
                Customer = "test",
                FactorNo = 324,
                FactorDate = DateOnly.FromDateTime(DateTime.Now),
                DelivaryType = 1,
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Factor", newFactor);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdFactor = await response.Content.ReadFromJsonAsync<AddFactorCommand>();
            createdFactor.Should().NotBeNull();
        }
        [Fact]
        public async Task UpdateFactor_ReturnsOk()
        {
            // Arrange
            var UpdateFactor = new Factor
            {
                FactorId = 1,
                Customer = "test",
                FactorNo = 324,
                FactorDate = DateOnly.FromDateTime(DateTime.Now),
                DelivaryType = 1,
            };
            // Act
            var response = await _client.PutAsJsonAsync("/api/Factor", UpdateFactor);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
