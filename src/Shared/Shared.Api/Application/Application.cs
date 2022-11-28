using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Api.Extensions;
using Shared.Api.Middleware;

namespace Shared.Api.Application;

public class Application
{
    private readonly AppDefinition _appDefinition;

    internal Application(AppDefinition appDefinition)
    {
        _appDefinition = appDefinition;
    }

    public async Task Start(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (_appDefinition.WithSwagger)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        if (_appDefinition.WithAuthorization) builder.Services.WithAuthorization(builder.Configuration);
        if (_appDefinition.WithAutentication) builder.Services.WithAuthentication(builder.Configuration);

        _appDefinition.ConfigureServices?.Invoke(builder.Services, builder.Configuration);

        using var app = builder.Build();

        app.Use(ExceptionHandler.Handle);

        _appDefinition.ConfigureWebApplication?.Invoke(app);

        if (_appDefinition.WithSwagger && app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (_appDefinition.WithAutentication) app.UseAuthentication();
        if (_appDefinition.WithAuthorization) app.UseAuthorization();

        await app.RunAsync();
    }

    public static IAppBuilder Create()
    {
        return new AppBuilder();
    }
}
