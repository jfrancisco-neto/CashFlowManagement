using Account.Domain.Model;

namespace Account.Api.Response;

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
        Claims = user.Claims.Select(c => new UserClaimResponse(c)).ToList();
    }

    public string Id { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<UserClaimResponse> Claims { get; set; }
}
