using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Src.api_net8.Application.Context;
using Src.api_net8.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.api_net8.Application.ProductFeature.Queries.FindProduct
{
    public class FindProductQueryHandler : IRequestHandler<FindProductQuery, Product>
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;
        public FindProductQueryHandler(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Product> Handle(FindProductQuery request, CancellationToken cancellationToken)
        {
            var person = _context.Products.Where(a => a.ProductId == request.ProductId).FirstOrDefault();
            if (person == null) return null;
            return person;
        }
    }
}
