using Shared.Entities.Model;

namespace Transaction.Domain.Model;

public class TransactionEntry : IEntity
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public string CreatedBy { get; set;}
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
}
