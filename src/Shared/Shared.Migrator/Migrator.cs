
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shared.Migrator;

public abstract class Migrator<D> where D : DbContext
{
    protected virtual void OnConfigureServices(HostBuilderContext builder, IServiceCollection services)
    { }

    protected virtual Task OnBeforeMigration(IHost host, DbContext dbcontext)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnAfterMigration(IHost host, DbContext dbcontext)
    {
        return Task.CompletedTask;
    }

    public async Task Migrate()
    {
        using var host = Host
            .CreateDefaultBuilder()
            .ConfigureServices((builder, services) => OnConfigureServices(builder, services))
            .Build();
        
        using var scope = host.Services.CreateScope();
        var dbcontext = scope.ServiceProvider.GetRequiredService<D>();

        await OnBeforeMigration(host, dbcontext);

        await dbcontext.Database.MigrateAsync();

        await OnAfterMigration(host, dbcontext);
    }
}
