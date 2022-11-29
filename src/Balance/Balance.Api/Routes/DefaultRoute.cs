using Balance.Api.Response;
using Balance.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Response;

namespace Balance.Api.Routes;

public static class DefaultRoute
{
    public static void MapRoutes(this WebApplication app)
    {
        app.MapGet("/{date}", GetBalance).RequireAuthorization("ListBalancePolicy");
    }

    public static async Task<IResult> GetBalance(
        IBalanceService balanceService,
        [FromRoute] DateTime? date)
    {
        if (!date.HasValue)
        {
            return Results.UnprocessableEntity(new ErrorResponse
            {
                Code = "date",
                Description = "Is required."
            });
        }

        var balance = await balanceService.GetByDate(DateOnly.FromDateTime(date.Value));

        if (balance is null)
        {
            return Results.NoContent();
        }

        return Results.Ok(new BalanceEntryResponse(balance));
    }
}
