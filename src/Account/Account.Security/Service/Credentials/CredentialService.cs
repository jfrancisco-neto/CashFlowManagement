using Account.Security.Model;
using Account.Security.Options;

namespace Account.Security.Service;

public class CredentialService : ICredentialService
{
    private readonly PasswordHasher _hasher;
    private readonly CredentialOptions _options;

    public CredentialService(CredentialOptions options)
    {
        _hasher = new PasswordHasher();
        _options = options;
    }

    public bool CheckCredential(string password, Credential credential)
    {
        var hash = _hasher.Hash(password, credential.Salt, _options.SecretSalt);

        return hash == credential.Hash;
    }

    public Credential CreateCredential(string password)
    {
        var salt = _hasher.CreateRandomString(_options.SaltLength);
        var hash = _hasher.Hash(password, salt, _options.SecretSalt);

        return new Credential
        {
            Hash = hash,
            Salt = salt
        };
    }
}
