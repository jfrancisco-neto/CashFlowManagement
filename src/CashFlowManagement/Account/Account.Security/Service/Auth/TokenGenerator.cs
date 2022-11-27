using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Account.Security.Model;
using Account.Security.Options;
using Microsoft.IdentityModel.Tokens;

namespace Account.Security.Service;

public class TokenGenerator : ITokenGenerator
{
    private readonly TokenGeneratorOptions _options;

    public TokenGenerator(TokenGeneratorOptions options)
    {
        _options = options;
    }

    public Token GenerateToken(TokenCredential credential)
    {
        var key = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(_options.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = credential.Claims?.Select(c => new Claim(c.Item1, c.Item2));
        var expirationDate = DateTime.UtcNow.Add(_options.TokenExpirationTime);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expirationDate,
            signingCredentials: credentials);

        return new Token
        {
            Value = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expirationDate
        };
    }
}
