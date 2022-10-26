using Lamar;
using MediatR;
using FluentValidation;
using DotNetProject.Application.Models;

namespace DotNetProject.Api.Configurations.Extensions
{
    public static class DependencyInjectionConfigurationExtensions
    {
        internal static void AddDependencyInjection(this ServiceRegistry services, IConfiguration configuration)
        {
            ((IServiceCollection)services).Configure<EnvironmentConfiguration>(configuration);

            services.Scan(_ =>
            {
                _.Assembly("DotNetProject.Application");
                _.Assembly("DotNetProject.Infrastructure");
                _.AddAllTypesOf<IValidator>();
                _.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                _.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                _.WithDefaultConventions();
                _.LookForRegistries();
            });
            
            services.AddTransient<IMediator, Mediator>();
            services.For<ServiceFactory>().Use(ctx => ctx.GetInstance);
        }
    }
}
