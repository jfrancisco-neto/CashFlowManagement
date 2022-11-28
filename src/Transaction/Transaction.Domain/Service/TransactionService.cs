using Shared.Entities.Model;
using Transaction.Domain.Model;
using Transaction.Domain.Repository;

namespace Transaction.Domain.Service;

public class TransactionService : ITransactionService
{
    private ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task Create(TransactionEntry transaction)
    {
        await _repository.Add(transaction);
    }

    public async Task<EntryCollection<TransactionEntry>> List(DateTime begin, DateTime end, int offset)
    {
        return await _repository.ListBetween(begin.ToUniversalTime(), end.ToUniversalTime(), offset, 100);
    }
}
