using CorrelationId.DependencyInjection;
using CorrelationId.HttpClient;
using DotNetProject.Api.HealthChecks;
using DotNetProject.Api.Middleware.Logging;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDefaultCorrelationId();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc();
builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
builder.Services.AddApiVersioning(o =>
{
    o.ReportApiVersions = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddOptions();
builder.Services.AddHttpClient(string.Empty)
    .AddCorrelationIdForwarding();

builder.Services.AddHealthChecks().AddCheck<ReadinessCheck>("DotNetProject readiness", tags: new[] { "readiness" });
builder.Services.AddCustomizedLogging();
builder.Services.AddHealthChecks();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
