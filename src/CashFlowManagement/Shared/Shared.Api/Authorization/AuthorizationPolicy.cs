namespace Shared.Api.Authorization;

public class AuthorizationPolicy
{
    public string Name { get; set; }
    public ICollection<AuthorizationPolicyClaim> RequiredClaims { get; set; }
}
