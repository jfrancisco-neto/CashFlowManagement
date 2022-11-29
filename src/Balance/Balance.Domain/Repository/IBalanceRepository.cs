using Balance.Domain.Model;
using Shared.Entities.Repository;

namespace Balance.Domain.Repository;

public interface IBalanceRepository : IEntityRepository<BalanceEntry>
{
    Task<BalanceEntry> GetByDate(DateOnly date);
}
