namespace Shared.Api.Authorization;

public class AuthorizationPolicyClaim
{
    public string Type { get; set; }
    public ICollection<string> ValidValues { get; set; }
}
