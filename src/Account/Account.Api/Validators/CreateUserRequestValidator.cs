using Account.Api.Request;
using FluentValidation;

namespace Account.Api.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(p => p.Login).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Name).NotEmpty().MaximumLength(2048);
        RuleFor(p => p.Password).NotEmpty().MaximumLength(100);
        RuleForEach(p => p.Claims).SetValidator(new CreateUserClaimRequestValidator());
    }
}
