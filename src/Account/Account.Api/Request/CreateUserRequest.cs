using Account.Domain.Model;

namespace Account.Api.Request;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public ICollection<CreateUserClaimRequest> Claims { get; set; }

    public User ToDomainUser()
    {
        return new User
        {
            Name = Name,
            Login = Login,
            Password = Password,
            Claims = Claims?.Select(c => c.ToDomainClaim()).ToList()
        };
    }
}
