using Balance.Api.Routes;
using Balance.IOC.Extensions;
using Shared.Api.Application;

await Application
    .Create()
    .ConfigureServices((services, configuration) => services.AddAllServices(configuration))
    .ConfigureWebApplication(app =>
    {
        app.MapRoutes();
    })
    .WithAuthentication()
    .WithAuthorization()
    .Build()
    .Start(args);
