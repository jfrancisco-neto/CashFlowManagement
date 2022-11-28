namespace Account.Domain.Model;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Active { get; set; } = true;
    public ICollection<UserClaim> Claims { get; set; }
}
