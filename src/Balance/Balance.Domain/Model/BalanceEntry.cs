using Shared.Entities.Model;

namespace Balance.Domain.Model;

public class BalanceEntry : IUpdatableEntity
{
    public long Id { get;  set; }
    public decimal TotalAmount { get; set; }
    public int EntriesCount { get; set; }
    public DateOnly Date { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public void AddTransaction(TransactionEntry entry)
    {
        TotalAmount += entry.Amount;
        EntriesCount++;
    }
}
