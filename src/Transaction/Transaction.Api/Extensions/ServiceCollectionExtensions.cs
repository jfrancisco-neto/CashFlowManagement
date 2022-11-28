using FluentValidation;
using Transaction.Api.Request;
using Transaction.Api.Validators;

namespace Transaction.Api.Extensions;

public static class ServicCollectionExtensions
{
    public static IServiceCollection AddRequestValidators(this IServiceCollection services)
    {
        return services
            .AddSingleton<IValidator<ListTransactionByDateRequest>, ListTransactionByDateRequestValidator>()
            .AddSingleton<IValidator<CreateTransactionRequest>, CreateTransactionRequestValidator>();
    }
}
