using Account.Domain.Repository;
using Account.Domain.Service;
using Account.Persistence.DatabaseContext;
using Account.Persistence.Repository;
using Account.Security.Options;
using Account.Security.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Account.IOC.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection MapOptions<T>(
        this IServiceCollection services,
        IConfiguration configuration)
        where T : class
    {
        services.AddSingleton<T>(sp => sp.GetRequiredService<IOptions<T>>().Value);
        services.Configure<T>(configuration);

        return services;
    }

    public static IServiceCollection AddAllServices(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddSecurity(configuration)
            .AddPersistence(configuration)
            .AddDomain(configuration);

    public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        => services
            .MapOptions<CredentialOptions>(configuration.GetSection("Security:Credential"))
            .MapOptions<IdHasherOptions>(configuration.GetSection("Security:IdHasher"))
            .MapOptions<TokenGeneratorOptions>(configuration.GetSection("Security:TokenGenerator"))
            .AddTransient<IIdHasher, IdHasher>()
            .AddTransient<ICredentialService, CredentialService>()
            .AddTransient<ITokenGenerator, TokenGenerator>();

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddTransient<IUserRepository, UserRepository>()
            .AddDbContextPool<PersistenceContext>(
                builder => builder.UseNpgsql(configuration.GetConnectionString("Account")));

    public static IServiceCollection AddDomain(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddTransient<IUserService, UserService>();
}
