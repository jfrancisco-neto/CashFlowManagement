using Microsoft.EntityFrameworkCore;
using Shared.Entities.Model;
using Shared.Persistence.Repository;
using Transaction.Domain.Model;
using Transaction.Domain.Repository;
using Transaction.Persistence.DatabaseContext;

namespace Transaction.Persistence.Repository;

public class TransactionRepository : EntityRepository<TransactionEntry, PersistenceContext>, ITransactionRepository
{
    public TransactionRepository(PersistenceContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<EntryCollection<TransactionEntry>> ListBetween(
        DateTime begin,
        DateTime end,
        int offset,
        int limit)
    {
        var query = DbContext.Transactions
            .Where(t => t.CreatedAt >= begin && t.CreatedAt <= end);

        var items = await query
            .OrderBy(t => t.Id)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        var count = await query.LongCountAsync();

        return new EntryCollection<TransactionEntry>
        {
            Items = items,
            Total = count
        };
    }
}
