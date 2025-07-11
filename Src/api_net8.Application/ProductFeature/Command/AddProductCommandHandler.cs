using api_.net9.Common.Dto;
using api_net9.Application.Context;
using api_net9.Domain.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net9.Application.ProductFeature.Command
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ServiceResponseDto<string>>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;
        public AddProductCommandHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponseDto<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var serviceResponse = new ServiceResponseDto<string>();
            var product = _mapper.Map<Product>(request);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            serviceResponse.Message = $" با موفقیت ایجاد شد {product.ProductName} محصول";
            serviceResponse.Data = product.ProductName;
            return serviceResponse;

        }

    }
}
