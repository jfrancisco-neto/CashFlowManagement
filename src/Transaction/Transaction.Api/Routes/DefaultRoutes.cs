using Microsoft.AspNetCore.Mvc;
using Shared.Api.Extensions;
using Transaction.Api.Request;
using Transaction.Api.Response;
using Transaction.Domain.Model;
using Transaction.Domain.Service;

namespace Transaction.Api.Routes;

public static class DefaultRoutes
{
    public static void MapRoutes(this WebApplication app)
    {
        app.MapPost("entry", CreateTransaction).RequireAuthorization("CreateTransaction");
        app.MapGet("entry", ListTransactions).RequireAuthorization("ListTransactionPolicy");
    }

    private static async Task<IResult> CreateTransaction(
        HttpContext httpContext,
        ITransactionService transactionService,
        CreateTransactionRequest createTransactionRequest)
    {
        var transaction = new TransactionEntry
        {
            Amount = createTransactionRequest.Amount.Value,
            Description = createTransactionRequest.Description,
            CreatedBy = httpContext.GetUserId()
        };

        await transactionService.Create(transaction);

        return Results.Ok(new TransactionResponse(transaction));
    }

    private static async Task<IResult> ListTransactions(
        ITransactionService transactionService,
        [AsParameters] ListTransactionByDateRequest query)
    {
        var transactions = await transactionService.List(query.Begin.Value, query.End.Value, query.Offset.Value);

        return Results.Ok(new TransactionCollectionResponse(transactions));
    }
}
