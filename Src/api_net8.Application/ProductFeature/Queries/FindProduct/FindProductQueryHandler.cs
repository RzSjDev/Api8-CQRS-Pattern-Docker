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

namespace api_net9.Application.ProductFeature.Queries.FindProduct
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
