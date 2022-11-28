using FluentValidation;
using Transaction.Api.Request;

namespace Transaction.Api.Validators;

public class CreateTransactionRequestValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionRequestValidator()
    {
        RuleFor(p => p.Amount).NotNull().GreaterThan(0);
        RuleFor(p => p.Description).NotEmpty().MaximumLength(100);
    }
}
