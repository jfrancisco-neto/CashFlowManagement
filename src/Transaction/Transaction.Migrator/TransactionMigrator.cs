using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Migrator;
using Transaction.IOC.Extensions;
using Transaction.Persistence.DatabaseContext;

namespace Transaction.Migrator;

public class TransactionMigrator : Migrator<PersistenceContext>
{
    protected override void OnConfigureServices(HostBuilderContext builder, IServiceCollection services)
    {
        base.OnConfigureServices(builder, services);

        services.AddPersistence(builder.Configuration);
    }
}