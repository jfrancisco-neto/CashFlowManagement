using Balance.Domain.Service;
using MassTransit;
using Shared.Contract.Message;

namespace Balance.MessageBroker;

public class EventConsumer : IConsumer<TransactionCreatedMessage>
{
    private readonly IBalanceService _balanceService;

    public EventConsumer(IBalanceService balanceService)
    {
        _balanceService = balanceService;
    }

    public async Task Consume(ConsumeContext<TransactionCreatedMessage> context)
    {
        await _balanceService.AddTransaction(new Domain.Model.TransactionEntry
        {
            Id = context.Message.Id,
            Amount = context.Message.Amount,
            CreatedAt = context.Message.CreatedAt
        });
    }
}
