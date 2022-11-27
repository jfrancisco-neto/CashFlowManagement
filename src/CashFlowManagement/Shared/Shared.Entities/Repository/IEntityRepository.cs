using Shared.Entities.Model;

namespace Shared.Entities.Repository;

public interface IEntityRepository<T> where T : IEntity
{
    Task Add(T t);
    Task Update(T t);
    Task Remove(T t);
    Task<T> GetById(long id);
    Task<long> Count();
    Task<ICollection<T>> List(int offset, int limit);
}
