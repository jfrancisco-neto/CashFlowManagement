namespace Account.Service;

using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Account.Model;
using Account.Repository;

public class UserService : IUserService
{
    private AuthenticationConfiguration _authenticationConfiguration;
    private IUserRepository _repository;

    public UserService(IUserRepository repository, AuthenticationConfiguration authenticationConfiguration)
    {
        _repository = repository;
        _authenticationConfiguration = authenticationConfiguration;
    }

    public async Task Add(User user)
    {
        user.Salt = CreateSalt();
        user.Password = HashStrings(user.Password, user.Salt, _authenticationConfiguration.Pepper);

        await _repository.Add(user);

        user.Password = null;
        user.Salt = null;
    }

    public async Task<User> ChangePassword(long userId, string oldPassword, string newPassword)
    {
        var user = await  _repository.GetById(userId);

        if (!CheckPasswordMatch(user, oldPassword))
        {
            throw new ArgumentException("Invalid password.");
        }

        user.Password = newPassword;

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
            throw new ArgumentException("User not found.");
        }

        user.Active = activeState;

        await _repository.Update(user);

        user.Password = null;
        user.Salt = null;

        return user;
    }

    public async Task<User> Login(string login, string password)
    {
        var user = await _repository.GetByLogin(login);

        if (user is null)
        {
            throw new ArgumentException("User not found");
        }

        if (!CheckPasswordMatch(user, password))
        {
            throw new ArgumentException("Login or password are invalid.");
        }

        user.Password = null;
        user.Salt = null;

        return user;
    }

    private bool CheckPasswordMatch(User user, string originalPassword)
    {
        return user.Password == HashStrings(originalPassword, user.Salt, _authenticationConfiguration.Pepper);
    }

    private void CreateSaltAndHash(User user)
    {
        user.Salt = CreateSalt();
        user.Password = HashStrings(user.Password, user.Salt, _authenticationConfiguration.Pepper);
    }

    private static string HashStrings(params string[] strings)
    {
        var byteStrings = strings.Select(s => Encoding.Unicode.GetBytes(s)).ToArray();
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
        var uuid = Guid.NewGuid().ToString();
        var bytes = Encoding.Unicode.GetBytes(uuid);

        return Convert.ToBase64String(bytes);
    }
}
