using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Transaction.Domain.Repository;
using Transaction.Domain.Service;
using Transaction.Persistence.DatabaseContext;
using Transaction.Persistence.Repository;

namespace Transaction.IOC.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllServices(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddPersistence(configuration)
            .AddDomain();

    private static IServiceCollection MapOptions<T>(
        this IServiceCollection services,
        IConfiguration configuration)
        where T : class
    {
        services.AddSingleton<T>(sp => sp.GetRequiredService<IOptions<T>>().Value);
        services.Configure<T>(configuration);

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddTransient<ITransactionRepository, TransactionRepository>()
            .AddDbContextPool<PersistenceContext>(builder =>
                builder.UseNpgsql(configuration.GetConnectionString("Transaction")));

    private static IServiceCollection AddDomain(this IServiceCollection services)
        => services .AddTransient<ITransactionService, TransactionService>();
}
