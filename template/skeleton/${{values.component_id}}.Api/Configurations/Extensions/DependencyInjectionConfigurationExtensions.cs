using Lamar;
using MediatR;
using FluentValidation;
using ${{values.component_id}}.Application.Models;
using ${{values.component_id}}.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ${{values.component_id}}.Api.Configurations.Extensions
{
    public static class DependencyInjectionConfigurationExtensions
    {
        internal static void AddDependencyInjection(this ServiceRegistry services, IConfiguration configuration)
        {
            ((IServiceCollection)services).Configure<EnvironmentConfiguration>(configuration);

            services.Scan(_ =>
            {
                _.Assembly("${{values.component_id}}.Application");
                _.Assembly("${{values.component_id}}.Infrastructure");
                _.AddAllTypesOf<IValidator>();
                _.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                _.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                _.WithDefaultConventions();
                _.LookForRegistries();
            });

            services.AddTransient<IMediator, Mediator>();
            services.For<ServiceFactory>().Use(ctx => ctx.GetInstance);

            // its necessary to use with EF Dbcontext, if you need use only Dapper remove this code
            var env = services.BuildServiceProvider().GetRequiredService<IOptions<EnvironmentConfiguration>>();
            services.AddDbContext<ExampleDbContext>(options =>options.UseSqlServer(env.Value.SQL_CONNECTION_STRING));
        }
    }
}
