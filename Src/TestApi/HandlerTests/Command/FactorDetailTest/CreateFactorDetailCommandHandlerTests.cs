using api_.net9.Common.Dto;
using api_net9.Application.FactorDetailFeature.Command.AddCommand;
using api_net9.Application.FactorFeature.Command.EditCommand;
using api_net9.Domain.Models;
using api_net9.Infrastructure.context;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace Src.TestApi.HandlerTests.Command.FactorDetailTest
{

    public class CreateFactorDetailCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IMediator _mediator;

        public CreateFactorDetailCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new DataContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddFactorDetailCommand, FactorDetail>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldCreateNewProductDetail()
        {
            // Arrange
            var serviceResponse = new ServiceResponseDto<int>();
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<EditFactorWithIdCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(serviceResponse);
            var handler = new AddFactorDetailCommandHandler(_context, _mapper, mediatorMock.Object);


            var NewProduct = new Product
            {
                ProductCode = "3",
                ProductName = "Test",
                Unit = "56",
                ChangeDate = DateTime.Now
            };
            var Factor = new Factor
            {
                FactorNo = 3,
                FactorDate = DateOnly.FromDateTime(DateTime.Now),
                Customer = "test",
                DelivaryType = 2,
            };
            await _context.Products.AddAsync(NewProduct);
            await _context.SaveChangesAsync();
            await _context.Factors.AddAsync(Factor);
            await _context.SaveChangesAsync();
            var command = new AddFactorDetailCommand(Factor.FactorId, NewProduct.ProductId, "test", 3, 3000);
            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.succsess.Should().Be(true);

        }
    }

}
