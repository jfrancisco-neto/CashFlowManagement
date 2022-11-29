using Balance.Domain.Model;
using Balance.Domain.Repository;

namespace Balance.Domain.Service;

public class BalanceService : IBalanceService
{
    private readonly IBalanceRepository _repository;

    public BalanceService(IBalanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<BalanceEntry> AddTransaction(TransactionEntry transaction)
    {
        var date = DateOnly.FromDateTime(transaction.CreatedAt.Date);
        var balance = await _repository.GetByDate(date);

        if (balance is null)
        {
            balance = new BalanceEntry
            {
                Date = date
            };
        }

        balance.AddTransaction(transaction);

        if (balance.Id == 0) await _repository.Add(balance);
        else await _repository.Update(balance);

        return balance;
    }

    public async Task<BalanceEntry> GetByDate(DateOnly date)
    {
        return await _repository.GetByDate(date);
    }
}
