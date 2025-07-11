using api_net9.Application.Behaviors;
using api_net9.Application.FactorDetailFeature.Command.AddCommand;
using api_net9.Application.FactorDetailFeature.Command.EditCommand;
using api_net9.Application.FactorFeature.Command.AddCommand;
using api_net9.Application.FactorFeature.Command.EditCommand;
using api_net9.Application.ProductFeature.Command;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace api_net9.Application
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
