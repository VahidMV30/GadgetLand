using System.Net;
using System.Text.Json;

namespace GadgetLand.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContent)
    {
        try
        {
            await next(httpContent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Internal Server Error.");
            await HandleExceptionAsync(httpContent, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            errors = new[] {
                new {
                    code = "InternalServerError",
                    description="خطای داخلی سرور رخ داده است."
                }
            }
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
