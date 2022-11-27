using Account.Domain.Model;

namespace Account.Api.Response;

public class UserClaimResponse
{
    public UserClaimResponse(UserClaim claim)
    {
        Type = claim.Type;
        Value = claim.Value;
    }

    public string Type { get; set; }
    public string Value { get; set; }
}
