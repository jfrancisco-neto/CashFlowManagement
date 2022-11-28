using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Api.Application;

public class AppBuilder : IAppBuilder
{
    private AppDefinition _appDefinition = new AppDefinition();

    public Application Build()
    {
        return new Application(_appDefinition);
    }

    public IAppBuilder ConfigureServices(Action<IServiceCollection, IConfiguration> action)
    {
        _appDefinition.ConfigureServices = action;
        
        return this;
    }

    public IAppBuilder ConfigureWebApplication(Action<WebApplication> action)
    {
        _appDefinition.ConfigureWebApplication = action;
        
        return this;
    }

    public IAppBuilder WithAuthentication()
    {
        _appDefinition.WithAutentication = true;
        
        return this;
    }

    public IAppBuilder WithAuthorization()
    {
        _appDefinition.WithAuthorization = true;
        
        return this;
    }

    public IAppBuilder WithSwagger()
    {
        _appDefinition.WithSwagger = true;
        
        return this;
    }
}

