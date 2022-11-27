namespace Transaction.Domain.Model;

public class TransactionEntryCollection
{
    public long Total { get; set; }
    public ICollection<TransactionEntry> Entries { get; set; }
}
