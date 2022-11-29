
using Balance.IOC.Extensions;
using Balance.Persistence.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Migrator;

namespace Balance.Migrator;

public class BalanceMigrator : Migrator<PersistenceContext>
{
    protected override void OnConfigureServices(HostBuilderContext builder, IServiceCollection services)
    {
        services.AddPersistence(builder.Configuration);
    }
}
