
using Account.Domain.Exceptions;
using Account.Domain.Model;
using Account.Domain.Service;
using Account.IOC.Extensions;
using Account.Migrator.Options;
using Account.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((builder, services) =>
    {
        services.AddPersistence(builder.Configuration);
        services.AddDomain(builder.Configuration);
        services.AddSecurity(builder.Configuration);
        services.MapOptions<MigratorOptions>(builder.Configuration.GetSection("Migrator:SuperUser"));
    })
    .Build();

using var scope = host.Services.CreateScope();
var dbcontext = scope.ServiceProvider.GetRequiredService<PersistenceContext>();
dbcontext.Database.Migrate();

var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
var superUserOptions = scope.ServiceProvider.GetRequiredService<MigratorOptions>();

try
{
    await userService.Add(new User
    {
        Login = superUserOptions.Login,
        Password = superUserOptions.Password,
        Name = superUserOptions.Name,
        Claims = superUserOptions.Claims
            ?.Select(c => new UserClaim
            {
                Type = c.Item1,
                Value = c.Item2
            }).ToList()
    });
}
catch(LoginUnavailable)
{
    Console.WriteLine("Super user already registered.");
}
