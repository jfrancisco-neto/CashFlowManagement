using Account.Domain.Exceptions;
using Account.Domain.Model;
using Account.Domain.Repository;
using Account.Security.Service;
using Account.Security.Model;

namespace Account.Domain.Service;

public class UserService : IUserService
{
    private IUserRepository _repository;
    private readonly ICredentialService _credentialService;

    public UserService(IUserRepository repository, ICredentialService credentialService)
    {
        _repository = repository;
        _credentialService = credentialService;
    }

    public async Task Add(User user)
    {
        var existingUser = await _repository.GetByLogin(user.Login);
        if (existingUser is not null)
        {
            throw new DomainException("Login is aready in use.");
        }

        FillWithNewCredential(user);

        user.Active = true;

        await _repository.Add(user);

        user.Password = null;
        user.Salt = null;
    }

    public async Task<User> ChangePassword(long userId, string originalPassword, string newPassword)
    {
        var user = await  _repository.GetById(userId);

        if (!CheckCredential(user, originalPassword))
        {
            throw new DomainException("Password does not match.");
        }

        user.Password = newPassword;

        FillWithNewCredential(user);

        await _repository.Update(user);

        return user;
    }

    public async Task<User> Login(string login, string password)
    {
        var user = await _repository.GetByLogin(login);

        if (user is null) return null;
        if (!CheckCredential(user, password)) return null;

        return user;
    }

    private bool CheckCredential(User user, string originalPassword)
    {
        var credential = new Credential
        {
            Hash = user.Password,
            Salt = user.Salt
        };

        return _credentialService.CheckCredential(originalPassword, credential);
    }

    private void FillWithNewCredential(User user)
    {
        var credential = _credentialService.CreateCredential(user.Password);

        user.Password = credential.Hash;
        user.Salt = credential.Salt;
    }

    public Task<User> GetUser(long id)
    {
        return _repository.GetById(id);
    }

    public Task<ICollection<User>> ListUsers(int offset)
    {
        return _repository.List(offset);
    }

    public Task<int> CountTotalUsers()
    {
        return _repository.Count();
    }
}
