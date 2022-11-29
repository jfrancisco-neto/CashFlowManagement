namespace Shared.Contract.Message;

public class TransactionCreatedMessage
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}
