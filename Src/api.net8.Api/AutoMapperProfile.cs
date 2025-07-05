using Src.api_net8.Application.FactorDetailFeature.Command.AddCommand;
using Src.api_net8.Application.FactorDetailFeature.Command.EditCommand;
using Src.api_net8.Application.FactorFeature.Command.AddCommand;
using Src.api_net8.Application.FactorFeature.Command.EditCommand;
using Src.api_net8.Application.ProductFeature.Command;
using Src.api_net8.Application.ProductFeature.Queries.FindProduct;
using Src.api_net8.Application.ProductFeature.Queries.GetAllProduct;
using Src.api_net8.Domain.Models;
using AutoMapper;
using Src.api_net8.Application.FactorDetailFeature.Command.AddCommand;

namespace api.net
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddFactorDetailCommand, FactorDetail>();
            CreateMap<EditFactorDetailCommand, FactorDetail>();
            CreateMap<EditFactorDetailWithIdCommand, FactorDetail>();
            CreateMap<AddFactorCommand, Factor>();
            CreateMap<EditFactorCommand, Factor>();
            CreateMap<EditFactorWithIdCommand, Factor>();
            CreateMap<AddProductCommand, Product>();
            CreateMap<FindProductQuery, Product>();
            CreateMap<GetAllProductQuery, Product>();
        }
    }
}