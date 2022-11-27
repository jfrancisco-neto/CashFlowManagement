using Transaction.Domain.Model;

namespace Transaction.Domain.Service;

public interface ITransactionService
{
    Task Create(TransactionEntry transaction);
    Task<TransactionEntryCollection> List(int offset);
}
