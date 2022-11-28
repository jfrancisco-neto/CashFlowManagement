using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AspnetCoreAuthorization = Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Shared.Api.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Shared.Api.Extensions;

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

    public static IServiceCollection WithAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .MapOptions<AuthorizationOptions>(configuration.GetSection(AuthorizationOptions.Section))
            .AddAuthorization(options =>
            {
                var authorizationOptions = new AuthorizationOptions();
                configuration.GetSection(AuthorizationOptions.Section).Bind(authorizationOptions);

                if (authorizationOptions.Policies is not null)
                {
                    foreach (var policy in authorizationOptions.Policies)
                    {
                        if (string.IsNullOrWhiteSpace(policy.Name) || policy.RequiredClaims.IsNullOrEmpty())
                        {
                            continue;
                        }

                        foreach (var claim in policy.RequiredClaims)
                        {
                            options.AddPolicy(policy.Name,builder => builder.RequireClaim(claim.Type, claim.ValidValues));
                        }
                    }
                }

                options.DefaultPolicy = new AspnetCoreAuthorization.AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireClaim("Id")
                    .RequireAuthenticatedUser()
                    .Build();
            });

        return services;
    }

    public static IServiceCollection WithAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .MapOptions<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.Section))
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                var authOptions = new AuthenticationOptions();
                configuration.GetSection(AuthenticationOptions.Section).Bind(authOptions);

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience =  true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAlgorithms = new []{ SecurityAlgorithms.HmacSha512 },
                    ValidAudience = authOptions.Audience,
                    ValidIssuer = authOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(authOptions.Key))
                };
            });

        return services;
    }
}
