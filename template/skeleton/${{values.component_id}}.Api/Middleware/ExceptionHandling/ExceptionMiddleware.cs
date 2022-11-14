using System.Net;
using FluentValidation;
using ILogger = Serilog.ILogger;

namespace ${{values.component_id}}.Api.Middleware.ExceptionHandling;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.Error(exception, exception.Message);

        var code = HttpStatusCode.InternalServerError; // 500 if unexpected
        if (exception is ValidationException) code = HttpStatusCode.BadRequest;
        context.Response.StatusCode = (int)code;

        string result = "An error has occurred";

        return context.Response.WriteAsync(result);
    }
}

