using System.ComponentModel.DataAnnotations;
using Account.Domain.Model;

namespace Account.Api.Requests;

public class CreateUserRequest
{
    [Required, MaxLength(2048)]
    public string Name { get; set; }

    [Required, MaxLength(100)]
    public string Login { get; set; }

    [Required, MaxLength(100)]
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
