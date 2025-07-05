using MediatR;
using Src.api_net8.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.api_net8.Application.ProductFeature.Queries.GetAllProduct
{
    public class GetAllProductQuery : IRequest<IEnumerable<Product>>;

}
