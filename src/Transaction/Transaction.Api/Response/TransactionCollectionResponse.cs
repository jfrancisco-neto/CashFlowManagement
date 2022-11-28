using Shared.Entities.Model;
using Transaction.Domain.Model;

namespace Transaction.Api.Response;

public class TransactionCollectionResponse
{
    public TransactionCollectionResponse(EntryCollection<TransactionEntry> entries)
    {
        Total = entries.Total;
        Items = entries.Items.Select(c => new TransactionResponse(c)).ToList();
    }

    public long Total { get; set; }
    public ICollection<TransactionResponse> Items { get; set; }
}
