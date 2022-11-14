using System.Text.Json.Serialization;
using CorrelationId;
using Lamar;
using Microsoft.AspNetCore.Mvc;
using ${{values.component_id}}.Api.Configurations.Extensions;
using ${{values.component_id}}.Api.Middleware.ExceptionHandling;
using CorrelationId.DependencyInjection;
using CorrelationId.HttpClient;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ${{values.component_id}}.Api.HealthChecks;
using ${{values.component_id}}.Api.Middleware.Logging;

namespace ${{values.component_id}}.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureContainer(ServiceRegistry services)
    {
        services.AddDefaultCorrelationId();
        services.AddHttpContextAccessor();
        services.AddMvc();
        services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
        services.AddApiVersioning(o =>
        {
            o.ReportApiVersions = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.AssumeDefaultVersionWhenUnspecified = true;
        });
        services.AddOptions();
        services.AddHttpClient(string.Empty)
            .AddCorrelationIdForwarding();

        services.AddSwagger();
        services.AddHealthChecks().AddCheck<ReadinessCheck>("${{values.component_id}} readiness", tags: new[] { "readiness" });
        services.AddCustomizedLogging();
        services.AddDependencyInjection(Configuration);

        services.AddHealthChecks();
        services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseCorrelationId();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwaggerDocumentation(provider);
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHealthChecks("/health");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
