using Account.Model;

namespace Account.Response;

public class UserResponse
{
    public UserResponse(User user, string userId)
    {
        Id = userId;
        Login = user.Login;
        Name = user.Name;
        CreatedAt = user.CreatedAt;
        UpdatedAt = user.UpdatedAt;
        Active = user.Active;
    }

    public string Id { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
