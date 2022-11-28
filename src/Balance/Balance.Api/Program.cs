using Shared.Api.Application;

await Application
    .Create()
    .ConfigureServices((services, configuration) =>
    {
    })
    .ConfigureWebApplication(app =>
    {
    })
    .WithAuthentication()
    .WithAuthorization()
    .Build()
    .Start(args);
