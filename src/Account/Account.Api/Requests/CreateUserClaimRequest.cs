using System.ComponentModel.DataAnnotations;
using Account.Domain.Model;

namespace Account.Api.Requests;

public class CreateUserClaimRequest
{
    public string Value { get; set; }
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
