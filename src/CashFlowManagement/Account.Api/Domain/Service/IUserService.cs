namespace Account.Service;

using Account.Model;

public interface IUserService
{
    Task Add(User user);
    Task<User> Login(string login, string password);
    Task<User> ChangePassword(long userId, string oldPassword, string newPassword);
    Task<User> SetActiveState(long userId, bool state);
}
