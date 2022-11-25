using Account.Model;
using Account.Repository;
using Microsoft.EntityFrameworkCore;

namespace Account.Persistence;

public class UserRepository : IUserRepository
{
    private readonly PersistenceContext _context;

    public UserRepository(PersistenceContext context)
    {
        _context = context;
    }

    public async Task Add(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public Task<int> Count()
    {
        return _context.Users.CountAsync();
    }

    public async Task<User> GetById(long id)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User> GetByLogin(string login)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Login == login);
    }

    public async Task<ICollection<User>> List(int offset)
    {
        return await _context.Users.Skip(offset).ToListAsync();
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);

        await _context.SaveChangesAsync();
    }
}
