using Account.Security.Model;

namespace Account.Security.Service;

public interface ITokenGenerator
{
    Token GenerateToken(TokenCredential credential);
}
