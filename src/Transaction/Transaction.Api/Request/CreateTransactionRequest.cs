using System.ComponentModel.DataAnnotations;

namespace Transaction.Api.Request;

public class CreateTransactionRequest
{
    [Required]
    public decimal? Amount { get; set; }

    [Required, MaxLength()]
    public string Description { get; set; }
}
