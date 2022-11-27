using Shared.Entities.Repository;
using Transaction.Domain.Model;

namespace Transaction.Domain.Repository;

public interface ITransactionRepository : IEntityRepository<TransactionEntry>
{
}