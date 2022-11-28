using Account.Security.Model;

namespace Account.Security.Service;

public interface ICredentialService
{
    Credential CreateCredential(string password);
    bool CheckCredential(string password, Credential credential);
}
