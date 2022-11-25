namespace Account.Repository;

using Account.Model;

public interface IUserRepository
{
    Task Add(User user);
    Task Update(User user);
    Task<User> GetById(long id);
    Task<User> GetByLogin(string login);
    Task<int> Count();
    Task<ICollection<User>> List(int offset);
}
