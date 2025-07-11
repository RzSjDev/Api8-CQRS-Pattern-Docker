using api_net9.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net9.Application.ProductFeature.Queries.GetAllProduct
{
    public class GetAllProductQuery : IRequest<IEnumerable<Product>>;

}
