using Account.Api.Middleware;
using Account.IOC.Extensions;
using Account.Routes;
using Shared.Api.Application;

await Application
    .Create()
    .ConfigureServices((services, configuration) => services.AddAllServices(configuration))
    .ConfigureWebApplication(app =>
    {
        app.Use(ExceptionHandler.Handle);
        app.MapRoutes();
    })
    .WithAuthentication()
    .WithAuthorization()
    .Build()
    .Start(args);
