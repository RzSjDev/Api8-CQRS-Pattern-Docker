using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Src.api_net8.Application.Behaviors;
using Src.api_net8.Application.FactorDetailFeature.Command.AddCommand;
using Src.api_net8.Application.FactorDetailFeature.Command.EditCommand;
using Src.api_net8.Application.FactorFeature.Command.AddCommand;
using Src.api_net8.Application.FactorFeature.Command.EditCommand;
using Src.api_net8.Application.ProductFeature.Command;
using System.Reflection;


namespace Src.api_net8.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblyContaining<AddProductCommandValidation>();
            services.AddValidatorsFromAssemblyContaining<AddFactorCommandValidation>();
            services.AddValidatorsFromAssemblyContaining<EditFactorCommandValidation>();
            services.AddValidatorsFromAssemblyContaining<AddFactorDetailCommandValidation>();
            services.AddValidatorsFromAssemblyContaining<EditFactorDetailCommandValidation>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;

        }
    }
}
