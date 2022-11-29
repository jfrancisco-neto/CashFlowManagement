using Balance.Domain.Repository;
using Balance.Domain.Service;
using Balance.Persistence.DatabaseContext;
using Balance.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Balance.IOC.Extensions;

public static class IOCExtensions
{
    public static IServiceCollection AddAllServices(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDomain()
            .AddPersistence(configuration);

    public static IServiceCollection AddDomain(
        this IServiceCollection services)
        => services
            .AddTransient<IBalanceService, BalanceService>();

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddTransient<IBalanceRepository, BalanceRepository>()
            .AddDbContextPool<PersistenceContext>(
                builder => builder.UseNpgsql(configuration.GetConnectionString("Balance")));
}
