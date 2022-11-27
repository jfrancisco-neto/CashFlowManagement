using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Account.Model;
using Account.Options;
using Microsoft.IdentityModel.Tokens;

namespace Account.Service;

public class JwtService : IJwtService
{
    private JwtOptions _options;

    public JwtService(JwtOptions options)
    {
        _options = options;
    }

    public JwtToken GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Login),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("Id", user.Id.ToString())
        };

        var expirationDate = DateTime.UtcNow.Add(_options.TokenExpirationTime);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expirationDate,
            signingCredentials: credentials);
        
        return new JwtToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expirationDate
        };
    }
}
