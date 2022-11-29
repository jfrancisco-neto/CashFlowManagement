using Balance.Domain.Model;

namespace Balance.Api.Response;

public class BalanceEntryResponse
{
    public BalanceEntryResponse(BalanceEntry entry)
    {
        Date = entry.Date;
        TotalAmount = entry.TotalAmount;
    }

    public DateOnly Date { get; set; }
    public decimal TotalAmount { get; set; }
}
