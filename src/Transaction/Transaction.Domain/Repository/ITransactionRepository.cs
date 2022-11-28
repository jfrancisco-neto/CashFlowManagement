using Shared.Entities.Model;
using Shared.Entities.Repository;
using Transaction.Domain.Model;

namespace Transaction.Domain.Repository;

public interface ITransactionRepository : IEntityRepository<TransactionEntry>
{
    Task<EntryCollection<TransactionEntry>> ListBetween(DateTime begin, DateTime end, int offset, int limit);
}
