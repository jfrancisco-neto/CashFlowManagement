namespace Account.Security.Model;

public class TokenCredential
{
    public string Id { get; set; }
    public ICollection<Tuple<string, string>> Claims { get; set; }
}
