using Microsoft.EntityFrameworkCore;
using Shared.Entities.Model;
using Shared.Entities.Repository;

namespace Shared.Persistence.Repository;

public abstract class EntityRepository<T, D> : IEntityRepository<T>
    where T : class, IEntity
    where D : DbContext
{
    protected EntityRepository(D dbContext)
    {
        DbContext = dbContext;
    }

    protected D DbContext { get; private set; }

    public async Task Add(T t)
    {
        DbContext.Set<T>().Add(t);

        await DbContext.SaveChangesAsync();
    }

    public Task<long> Count()
    {
        return DbContext.Set<T>().LongCountAsync();
    }

    public async Task<T> GetById(long id)
    {
        return await DbContext.Set<T>().FindAsync(id);
    }

    public async Task<ICollection<T>> List(int offset, int limit)
    {
        return await DbContext.Set<T>().Skip(offset).Take(limit).ToListAsync();
    }

    public async Task Remove(T t)
    {
        await Update(t);
    }

    public async Task Update(T t)
    {
        DbContext.Set<T>().Update(t);

        await DbContext.SaveChangesAsync();
    }
}