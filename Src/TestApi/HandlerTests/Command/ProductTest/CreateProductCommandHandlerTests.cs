
using api_net9.Application.ProductFeature.Command;
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

namespace Src.TestApi.HandlerTests.Command.ProductTest
{

    public class CreateProductCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CreateProductCommandHandlerTests()
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
        public async Task Handle_GivenValidRequest_ShouldCreateNewProduct()
        {
            // Arrange
            var handler = new AddProductCommandHandler(_context, _mapper);
            var command = new AddProductCommand("3", "Test", "56", DateTime.Now);
            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.succsess.Should().Be(true);

        }
    }

}
