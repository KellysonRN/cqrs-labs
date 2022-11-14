﻿using CorrelationId.Abstractions;
using Microsoft.Extensions.Options;
using ${{values.component_id}}.Application.Models;
using ILogger = Serilog.ILogger;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace ${{values.component_id}}.Api.Middleware.Logging
{
    public static class LoggingServiceFactory
    {
        public static IServiceCollection AddCustomizedLogging(this IServiceCollection sc)
        {
            var sp = sc.BuildServiceProvider();
            var httpAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var correlationAccessor = sp.GetRequiredService<ICorrelationContextAccessor>();
            var configuration = sp.GetRequiredService<IOptions<EnvironmentConfiguration>>();
            var logLevelStr = configuration.Value.LOG_LEVEL;

            var logLevel = Enum.TryParse(logLevelStr, out LogEventLevel level) ? level : LogEventLevel.Information;

            var formatter = new JsonLogFormatter();

            var conf = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.With(new HttpEnricher(httpAccessor, correlationAccessor))
                .MinimumLevel.ControlledBy(new LoggingLevelSwitch(logLevel))
                .MinimumLevel.Override("CorrelationId", LogEventLevel.Error);

            conf.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");
            // Add additional sinks (Azure, logentries, etc) here

            var serilog = conf.CreateLogger();
            Microsoft.Extensions.Logging.ILoggerFactory msLoggerFactory = new SerilogLoggerFactory(serilog);
            msLoggerFactory.AddProvider(new SerilogLoggerProvider(serilog));

            sc.AddSingleton<ILogger>(serilog);
            sc.AddSingleton(msLoggerFactory).AddSingleton(msLoggerFactory.CreateLogger("${{values.component_id}}"));

            return sc;
        }
    }
}
