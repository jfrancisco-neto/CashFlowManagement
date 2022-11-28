using Account.Api.Request;
using Account.Api.Validators;
using FluentValidation;

namespace Account.Api.Extensions;

public static class ServicCollectionExtensions
{
    public static IServiceCollection AddRequestValidators(this IServiceCollection services)
    {
        return services
            .AddSingleton<IValidator<LoginRequest>, LoginRequestValidator>()
            .AddSingleton<IValidator<CreateUserRequest>, CreateUserRequestValidator>();
    }
}
