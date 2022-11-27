namespace Account.Api.Response;

public class ErrorResponse
{
    public ErrorResponse(params string[] errors)
    {
        Errors = errors;
    }

    public ErrorResponse() { }

    public ICollection<string> Errors { get; set; }
}
