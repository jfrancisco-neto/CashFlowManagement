using FluentValidation;
using Transaction.Api.Request;

namespace Transaction.Api.Validators;

public class ListTransactionByDateRequestValidator : AbstractValidator<ListTransactionByDateRequest>
{
    public ListTransactionByDateRequestValidator()
    {
        RuleFor(p => p.Begin).NotNull();
        RuleFor(p => p.End).NotNull();
        RuleFor(p => p.Offset).GreaterThan(-1);
    }
}
