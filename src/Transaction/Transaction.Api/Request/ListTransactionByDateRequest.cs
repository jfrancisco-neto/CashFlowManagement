using System.ComponentModel.DataAnnotations;

namespace Transaction.Api.Request;

public class ListTransactionByDateRequest
{
    public DateTime? Begin { get; set; }
    public DateTime? End { get; set; }
    public int? Offset { get; set; }
}
