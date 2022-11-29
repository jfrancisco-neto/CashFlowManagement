using Shared.Contract.Message;
using Shared.Entities.Model;
using Transaction.Domain.Events;
using Transaction.Domain.Model;
using Transaction.Domain.Repository;

namespace Transaction.Domain.Service;

public class TransactionService : ITransactionService
{
    private ITransactionRepository _repository;
    private IEventEmitter _emitter;

    public TransactionService(ITransactionRepository repository, IEventEmitter emitter)
    {
        _repository = repository;
        _emitter = emitter;
    }

    public async Task Create(TransactionEntry transaction)
    {
        await _repository.Add(transaction);
        await EmmitEvent(transaction);
    }

    public async Task<EntryCollection<TransactionEntry>> List(DateTime begin, DateTime end, int offset)
    {
        return await _repository.ListBetween(begin.ToUniversalTime(), end.ToUniversalTime(), offset, 100);
    }

    private async Task EmmitEvent(TransactionEntry transaction)
    {
        await _emitter.Emit(new TransactionCreatedMessage
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            CreatedAt = transaction.CreatedAt,
            CreatedBy = transaction.CreatedBy
        });
    }
}
