using Shared.Contract.Message;

namespace Transaction.Domain.Events;

public interface IEventEmitter
{
    Task Emit(TransactionCreatedMessage message);
}