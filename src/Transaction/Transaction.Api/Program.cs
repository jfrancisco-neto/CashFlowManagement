
using Shared.Api.Application;
using Transaction.Api.Extensions;
using Transaction.Api.Routes;
using Transaction.IOC.Extensions;

await Application
    .Create()
    .ConfigureServices((services, configuration)
        => services
            .AddAllServices(configuration)
            .AddRequestValidators())
    .ConfigureWebApplication(app =>
    {
        app.MapRoutes();
    })
    .WithAuthentication()
    .WithAuthorization()
    .Build()
    .Start(args);
