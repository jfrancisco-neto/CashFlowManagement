namespace Account.Domain.Options;

public class AuthOptions
{
    public string Pepper { get; set; }
    public int SaltLength { get; set; }
}
