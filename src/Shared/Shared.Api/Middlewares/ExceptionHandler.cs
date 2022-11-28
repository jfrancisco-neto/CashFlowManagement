using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Api.Response;

namespace Shared.Api.Middleware;

public class ExceptionHandler
{
    public static async Task Handle(HttpContext context, RequestDelegate next)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandler>>();

        try
        {
            await next(context);
        }
        catch(Exception e)
        {
            logger.LogError(e, "Unhandled exception.");

            var errorMessage = "INTERNAL_SERVER_ERROR";

            context.Response.StatusCode = (int) System.Net.HttpStatusCode.InternalServerError;

            await context.Response.WriteAsJsonAsync(new ErrorResponse
            {
                Code = errorMessage,
                Description = "Unexpected error ocurred."
            });
        }
    }
}