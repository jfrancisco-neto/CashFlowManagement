using Account.Model;

namespace Account.Service;

public interface IJwtService
{
    JwtToken GenerateToken(User user);
}
