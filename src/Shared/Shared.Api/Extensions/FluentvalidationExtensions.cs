using FluentValidation.Results;
using Shared.Api.Response;

namespace Shared.Api.Extensions;

public static class FluentvalidationExtensions
{
    public static ErrorCollectionResponse ToErrorResponse(this ValidationResult result)
    {
        return new ErrorCollectionResponse
        {
            Errors = result.Errors
                .Select(e => new ErrorResponse
                {
                    Code = e.PropertyName,
                    Description = e.ErrorMessage
                })
                .ToList()
        };
    }
}