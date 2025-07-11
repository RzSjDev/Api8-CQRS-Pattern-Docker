using api_net9.Application.ProductFeature.Command;
using api_net9.Application.ProductFeature.Queries.FindProduct;
using api_net9.Domain.Models;
using api_net9.Infrastructure.context;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.TestApi.HandlerTests.Queries.ProductTest
{

    public class GetProductCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public GetProductCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new DataContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddProductCommand, Product>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_GivenValidId_ShouldGetProduct()
        {
            // Arrange
            var command = new Product
            {
                ProductCode = "3",
                ProductName = "Test",
                Unit = "56",
                ChangeDate = DateTime.Now
            };
            await _context.Products.AddAsync(command);
            await _context.SaveChangesAsync();
            var handler = new FindProductQueryHandler(_context, _mapper);
            // Act
            var result = await handler.Handle(new FindProductQuery(command.ProductId), default);

            // Assert
            result.Should().NotBeNull();
            result.ProductCode.Should().Be(command.ProductCode);
            result.ProductName.Should().Be(command.ProductName);
            result.ChangeDate.Should().Be(command.ChangeDate);
            result.Unit.Should().Be(command.Unit);

        }
    }

}
