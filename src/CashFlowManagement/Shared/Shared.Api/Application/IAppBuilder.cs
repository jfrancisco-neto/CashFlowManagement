using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Api.Application;

public interface IAppBuilder
{
    IAppBuilder ConfigureServices(Action<IServiceCollection, IConfiguration> action);
    IAppBuilder ConfigureWebApplication(Action<WebApplication> action);
    IAppBuilder WithAuthentication();
    IAppBuilder WithAuthorization();
    IAppBuilder WithSwagger();
    Application Build();
}
