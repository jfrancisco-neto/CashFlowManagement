using Account.Api.Requests;
using FluentValidation;

namespace Account.Api.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(p => p.Login).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Password).NotEmpty().MaximumLength(2048);
    }
}
