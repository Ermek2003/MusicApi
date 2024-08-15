using System.Net;

namespace WebApi.Middlewares;

public class ExeptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExeptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(exception.Message);
        }
    }
}
