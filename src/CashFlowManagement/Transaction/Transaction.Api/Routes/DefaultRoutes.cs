using Transaction.Api.Request;
using Transaction.Api.Response;
using Transaction.Domain.Model;
using Transaction.Domain.Service;

namespace Transaction.Api.Routes;

public static class DefaultRoutes
{
    public static void MapRoutes(this WebApplication app)
    {
        app.MapPost("transaction", DefaultRoutes.CreateTransaction);
    }

    private static async Task<IResult> CreateTransaction(
        HttpContext httpContext,
        ITransactionService transactionService,
        CreateTransactionRequest createTransactionRequest)
    {
        var transaction = new TransactionEntry
        {
            Amount = createTransactionRequest.Amount,
            Description = createTransactionRequest.Description,
            CreatedBy = httpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value
        };

        await transactionService.Create(transaction);

        return Results.Ok(new TransactionResponse(transaction));
    }
}
