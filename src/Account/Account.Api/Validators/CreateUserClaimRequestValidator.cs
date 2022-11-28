using Account.Api.Request;
using FluentValidation;

namespace Account.Api.Validators;

public class CreateUserClaimRequestValidator : AbstractValidator<CreateUserClaimRequest>
{
    public CreateUserClaimRequestValidator()
    {
        RuleFor(p => p.Type).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Value).NotEmpty().MaximumLength(100);
    }
}