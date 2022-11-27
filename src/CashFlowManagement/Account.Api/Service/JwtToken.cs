namespace Account.Service;

public class JwtToken
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}
