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

    public async Task<TransactionEntryCollection> List(int offset)
    {
        return new TransactionEntryCollection
        {
            Total = await _repository.Count(),
            Entries = await _repository.List(offset, 100)
        };
    }
}
