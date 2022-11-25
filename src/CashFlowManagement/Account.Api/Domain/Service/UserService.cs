namespace Account.Service;

using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Account.Options;
using Account.Domain.Exceptions;
using Account.Model;
using Account.Repository;

public class UserService : IUserService
{
    private AuthOptions _authOptions;
    private IUserRepository _repository;

    public UserService(IUserRepository repository, AuthOptions authOptions)
    {
        _repository = repository;
        _authOptions = authOptions;
    }

    public async Task Add(User user)
    {
        var existingUser = await _repository.GetByLogin(user.Login);
        if (existingUser is not null)
        {
            throw new DomainException("Login is aready in use.");
        }

        user.Salt = CreateSalt();
        user.Password = HashStrings(user.Password, user.Salt, _authOptions.Pepper);
        user.Active = true;
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await _repository.Add(user);

        user.Password = null;
        user.Salt = null;
    }

    public async Task<User> ChangePassword(long userId, string oldPassword, string newPassword)
    {
        var user = await  _repository.GetById(userId);

        if (!CheckPasswordMatch(user, oldPassword))
        {
            throw new DomainException("Invalid password.");
        }

        user.Password = newPassword;
        user.UpdatedAt = DateTime.UtcNow;

        CreateSaltAndHash(user);

        await _repository.Update(user);

        user.Password = null;
        user.Salt = null;

        return user;
    }

    public async  Task<User> SetActiveState(long userId, bool activeState)
    {
        var user = await _repository.GetById(userId);
        if (user is null)
        {
            throw new DomainException("User not found.");
        }

        user.Active = activeState;
        user.UpdatedAt = DateTime.UtcNow;

        await _repository.Update(user);

        user.Password = null;
        user.Salt = null;

        return user;
    }

    public async Task<User> Login(string login, string password)
    {
        var user = await _repository.GetByLogin(login);

        if (user is null) return null;
        if (!CheckPasswordMatch(user, password)) return null;

        user.Password = null;
        user.Salt = null;

        return user;
    }

    private bool CheckPasswordMatch(User user, string originalPassword)
    {
        return user.Password == HashStrings(originalPassword, user.Salt, _authOptions.Pepper);
    }

    private void CreateSaltAndHash(User user)
    {
        user.Salt = CreateSalt();
        user.Password = HashStrings(user.Password, user.Salt, _authOptions.Pepper);
    }

    private static string HashStrings(params string[] strings)
    {
        var byteStrings = strings.Select(s => Encoding.Unicode.GetBytes(s)).ToList();
        var totalSize = byteStrings.Sum(c => c.Length);
        var buffer = new byte[totalSize];
        var offset = 0;

        foreach (var i in byteStrings)
        {
            Buffer.BlockCopy(i, 0, buffer, offset, i.Length);
            offset += i.Length;
        }

        using var crypto = SHA512.Create();

        var hashedBuffer = crypto.ComputeHash(buffer);

        return Convert.ToBase64String(hashedBuffer);
    }

    private static string CreateSalt()
    {
        var uuid = Guid.NewGuid().ToByteArray();

        return Convert.ToBase64String(uuid);
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
