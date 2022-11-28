using System.ComponentModel.DataAnnotations;

namespace Transaction.Api.Request;

public class ListTransactionByDateRequest
{
    [Required]
    public DateTime? Begin { get; set; }

    [Required]
    public DateTime? End { get; set; }

    [Required]
    public int? Offset { get; set; }
}
