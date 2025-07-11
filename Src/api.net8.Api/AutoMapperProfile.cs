using AutoMapper;
using api_net9.Application.FactorFeature.Command.EditCommand;
using api_net9.Application.FactorFeature.Command.AddCommand;
using api_net9.Application.ProductFeature.Command;
using api_net9.Application.FactorDetailFeature.Command.AddCommand;
using api_net9.Application.ProductFeature.Queries.GetAllProduct;
using api_net9.Application.ProductFeature.Queries.FindProduct;
using api_net9.Application.FactorDetailFeature.Command.EditCommand;
using api_net9.Domain.Models;

namespace api_net9
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