using Balance.Domain.Repository;
using Balance.Domain.Model;
using Balance.Persistence.DatabaseContext;
using Shared.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Balance.Persistence.Repository;

public class BalanceRepository : EntityRepository<BalanceEntry, PersistenceContext>, IBalanceRepository
{
    public BalanceRepository(PersistenceContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<BalanceEntry> GetByDate(DateOnly date)
    {
        return await DbContext.BalanceEntries
            .Where(b => b.Date == date)
            .SingleOrDefaultAsync();
    }
}
