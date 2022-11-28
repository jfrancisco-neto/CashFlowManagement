namespace Shared.Api.Options;

public class AuthenticationOptions
{
    public const string Section  = "Application:Authentication";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
}
