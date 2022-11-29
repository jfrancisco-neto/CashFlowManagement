using MassTransit;
using Shared.Contract.Message;
using Transaction.Domain.Events;

namespace Transaction.MessageBroker;

public class EventEmitter : IEventEmitter
{
    private readonly ITopicProducer<TransactionCreatedMessage> _producer;

    public EventEmitter(ITopicProducer<TransactionCreatedMessage> producer)
    {
        _producer = producer;
    }

    public async Task Emit(TransactionCreatedMessage e)
    {
        await _producer.Produce(e);
    }
}
