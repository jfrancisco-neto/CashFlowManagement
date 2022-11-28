using Shared.Api.Authorization;

namespace Shared.Api.Options;

public class AuthorizationOptions
{
    public const string Section = "Application:Authorization";
    public ICollection<AuthorizationPolicy> Policies { get; set; }
}