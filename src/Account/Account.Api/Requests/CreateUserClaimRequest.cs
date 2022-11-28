using System.ComponentModel.DataAnnotations;
using Account.Domain.Model;

namespace Account.Api.Requests;

public class CreateUserClaimRequest
{
    [Required, MaxLength(100)]
    public string Value { get; set; }

    [Required, MaxLength(100)]
    public string Type { get; set; }

    public UserClaim ToDomainClaim()
    {
        return new UserClaim
        {
            Type = Type,
            Value = Value
        };
    }
}
