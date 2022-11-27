using Shared.Persistence.Repository;
using Transaction.Domain.Model;
using Transaction.Domain.Repository;
using Transaction.Persistence.DatabaseContext;

namespace Transaction.Persistence.Repository;

public class TransactionRepository : Repository<TransactionEntry, PersistenceContext>, ITransactionRepository
{
    public TransactionRepository(PersistenceContext dbContext)
        : base(dbContext)
    {
    }
}
