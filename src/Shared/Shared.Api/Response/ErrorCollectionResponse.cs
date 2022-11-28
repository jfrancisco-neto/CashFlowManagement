using Shared.Entities.Model;

namespace Shared.Api.Response;

public class ErrorCollectionResponse
{
    public ICollection<ErrorResponse> Errors { get; set; }
}
