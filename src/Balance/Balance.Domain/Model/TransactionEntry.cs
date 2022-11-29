namespace Balance.Domain.Model;

public class TransactionEntry
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}
