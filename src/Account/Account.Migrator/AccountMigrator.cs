using System.Text.Json;
using Account.Domain.Exceptions;
using Account.Domain.Model;
using Account.Domain.Service;
using Account.IOC.Extensions;
using Account.Migrator.Options;
using Account.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Migrator;

namespace Account.Migrator;

public class AccountMigrator : Migrator<PersistenceContext>
{
    protected override void OnConfigureServices(HostBuilderContext builder, IServiceCollection services)
    {
        services.AddAllServices(builder.Configuration);
        services.MapOptions<MigratorOptions>(builder.Configuration.GetSection("Migrator:SuperUser"));
    }

    protected override async Task OnAfterMigration(IHost host, DbContext dbcontext)
    {
        using var scope = host.Services.CreateScope();

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
    }
}
