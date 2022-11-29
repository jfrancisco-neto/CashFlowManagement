using Balance.Domain.Model;

namespace Balance.Domain.Service;

public interface IBalanceService
{
    Task<BalanceEntry> AddTransaction(TransactionEntry transaction);
    Task<BalanceEntry> GetByDate(DateOnly date);
}
