namespace Account.Domain.Model;

public class UserClaim
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; }
}
