using Account.Domain.Exceptions;
using Shared.Api.Response;

namespace Account.Api.Middleware;

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
            var statusCode = (int) System.Net.HttpStatusCode.InternalServerError;
            var errorCode = "INTERNAL_SERVER_ERROR";
            var errorMessage = "An unexpected error ocurred.";

            if (e is DomainException)
            {
                statusCode = (int) System.Net.HttpStatusCode.UnprocessableEntity;
                errorCode = "ERROR";
                errorMessage = e.Message;
                logger.LogInformation(e, "Domain Exception.");
            }
            else
            {
                logger.LogError(e, "Unhandled exception.");
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new ErrorResponse(errorCode, errorMessage));
        }
    }
}