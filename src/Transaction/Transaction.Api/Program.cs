
using Shared.Api.Application;
using Transaction.Api.Routes;
using Transaction.IOC.Extensions;

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
