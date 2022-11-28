using Shared.Entities.Model;
using Transaction.Domain.Model;

namespace Transaction.Domain.Service;

public interface ITransactionService
{
    Task Create(TransactionEntry transaction);
    Task<EntryCollection<TransactionEntry>> List(DateTime begin, DateTime end, int offset);
}
