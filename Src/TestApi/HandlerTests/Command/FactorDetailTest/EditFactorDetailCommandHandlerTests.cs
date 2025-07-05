using Src.api_.net8.Common.Enum;
using Src.api_net8.Application.Context;
using Src.api_net8.Application.FactorDetailFeature.Command.AddCommand;
using Src.api_net8.Application.FactorDetailFeature.Command.EditCommand;
using Src.api_net8.Application.FactorFeature.Command.AddCommand;
using Src.api_net8.Application.ProductFeature.Command;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Src.api_net8.Application.FactorDetailFeature.Command.EditCommand;
using Src.api_net8.Application.FactorFeature.Command.EditCommand;
using Src.api_net8.Domain.Models;
using Src.api_net8.Infrastructure.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Src.api_.net8.Common.Dto;

namespace Src.TestApi.HandlerTests.Command.FactorDetailTest
{

    public class EditFactorDetailCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IMediator _mediator;
        public EditFactorDetailCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new DataContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditFactorDetailWithIdCommand, FactorDetail>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldCreateNewProduct()
        {
            // Arrange

            var serviceResponse = new ServiceResponseDto<int>();
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<EditFactorWithIdCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(serviceResponse);
            var handler = new EditFactorDetailCommandHandler(_context, _mapper, mediatorMock.Object);
            var Factor = new Factor
            {
                FactorNo = 3,
                FactorDate = DateOnly.FromDateTime(DateTime.Now),
                Customer = "test",
                DelivaryType = 2,
            };
            var NewProduct = new Product
            {
                ProductCode = "3",
                ProductName = "Test",
                Unit = "56",
                ChangeDate = DateTime.Now
            };

            await _context.Factors.AddAsync(Factor);
            await _context.Products.AddAsync(NewProduct);
            await _context.SaveChangesAsync();
            var FactorDetail = new FactorDetail
            {
                FactorId = Factor.FactorId,
                ProductId = NewProduct.ProductId,
                ProductDescription = "test",
                Count = 5,
                UnitPrice = 4000
            };
            await _context.FactorDetails.AddAsync(FactorDetail);
            await _context.SaveChangesAsync();
            var command = new EditFactorDetailWithIdCommand(FactorDetail.FactorDetailId, Factor.FactorId,
                NewProduct.ProductId, "Test", 45, 2000, 0);
            //Act

            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.succsess.Should().Be(true);

        }
    }

}
