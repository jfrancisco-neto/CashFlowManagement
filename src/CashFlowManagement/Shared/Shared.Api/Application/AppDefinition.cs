using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Api.Application;

internal class AppDefinition
{
    public Action<IServiceCollection, IConfiguration> ConfigureServices { get; set; }
    public Action<WebApplication> ConfigureWebApplication { get; set; }
    public bool WithAutentication { get; set; }
    public bool WithAuthorization { get; set; }
    public bool WithSwagger { get; set; }
}

