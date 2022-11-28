using Account.Domain.Model;

namespace Account.Domain.Service;

public interface IUserService
{
    Task Add(User user);
    Task<User> Login(string login, string password);
    Task<User> ChangePassword(long userId, string oldPassword, string newPassword);
    Task<User> GetUser(long id);
    Task<ICollection<User>> ListUsers(int offset);
    Task<int> CountTotalUsers();
}
