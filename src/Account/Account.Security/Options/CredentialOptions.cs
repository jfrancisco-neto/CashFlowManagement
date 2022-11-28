namespace Account.Security.Options;

public class CredentialOptions
{
    public int SaltLength { get; set; }
    public string SecretSalt { get; set; }
}
