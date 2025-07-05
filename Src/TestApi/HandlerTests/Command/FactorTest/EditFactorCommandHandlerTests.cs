using Src.api_net8.Application.Context;
using Src.api_net8.Application.FactorFeature.Command.AddCommand;
using Src.api_net8.Application.FactorFeature.Command.EditCommand;
using Src.api_net8.Application.ProductFeature.Command;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Src.api_.net8.Common.Enum;
using Src.api_net8.Application.FactorFeature.Command.EditCommand;
using Src.api_net8.Domain.Models;
using Src.api_net8.Infrastructure.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.TestApi.HandlerTests.Command.FactorTest
{

    public class EditFactorCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public EditFactorCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new DataContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditFactorWithIdCommand, Factor>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldEditFactor()
        {
            // Arrange

            var Factor = new Factor
            {
                FactorNo = 3,
                FactorDate = DateOnly.FromDateTime(DateTime.Now),
                Customer = "test",
                DelivaryType = 2,
            };
            await _context.Factors.AddAsync(Factor);
            await _context.SaveChangesAsync();
            var handler = new EditFactorCommandHandler(_context, _mapper);
            var command = new EditFactorWithIdCommand(
                Factor.FactorId, 8, DateOnly.FromDateTime(DateTime.Now), "test", DelivaryType.peyk, 0);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.succsess.Should().Be(true);

        }
    }

}
