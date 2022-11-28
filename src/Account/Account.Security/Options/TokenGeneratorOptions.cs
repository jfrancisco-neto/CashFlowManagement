namespace Account.Security.Options;

public class TokenGeneratorOptions
{
    public string Key { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public TimeSpan TokenExpirationTime { get; set; }
}
