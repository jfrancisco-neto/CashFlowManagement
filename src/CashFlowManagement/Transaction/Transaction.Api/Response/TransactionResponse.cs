using Transaction.Domain.Model;

namespace Transaction.Api.Response;

public class TransactionResponse
{
    public TransactionResponse(TransactionEntry transaction)
    {
        Id = transaction.Id;
        Description = transaction.Description;
        Amount = transaction.Amount;
        CreatedAt = transaction.CreatedAt;
        CreatedBy = transaction.CreatedBy;
    }

    public long Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}