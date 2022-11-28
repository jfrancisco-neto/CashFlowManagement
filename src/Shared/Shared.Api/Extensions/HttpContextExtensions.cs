using Microsoft.AspNetCore.Http;

namespace Shared.Api.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserId(this HttpContext context)
    {
        return context?.User.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
    }
}
