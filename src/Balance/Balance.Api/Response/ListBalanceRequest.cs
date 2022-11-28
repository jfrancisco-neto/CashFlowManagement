namespace Balance.Api.Response;

public class BalanceResponse
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public decimal TotalAmount { get; set; }
}
