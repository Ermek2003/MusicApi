using System.Net;

namespace WebApi.Middlewares;

public class ExeptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExeptionHandlerMiddleware> _logger;

    public ExeptionHandlerMiddleware(RequestDelegate next, ILogger<ExeptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred during the request.");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(exception.Message);
        }
    }
}
