using Account.Domain.Service;
using Account.Requests;
using Account.Security.Service;
using Microsoft.AspNetCore.Mvc;
using Account.Api.Requests;
using Account.Api.Response;
using Account.Security.Model;
using Shared.Api.Response;

namespace Account.Routes;

public static class DefaultRoutes
{
    public static void MapRoutes(this WebApplication app)
    {
        app.MapPost("user", DefaultRoutes.CreateUser).RequireAuthorization("CreateUserPolicy");
        app.MapGet("user/{id}", DefaultRoutes.GetUser).RequireAuthorization("InspectUserPolicy");
        app.MapGet("user", DefaultRoutes.ListUsers).RequireAuthorization("InspectUserPolicy");
        app.MapPost("auth", DefaultRoutes.Login).AllowAnonymous();
    }

    public static async Task<IResult> CreateUser(
        IIdHasher hasher,
        HttpRequest httpRequest,
        IUserService userService,
        [FromBody] CreateUserRequest request)
    {
        var user = request.ToDomainUser();

        await userService.Add(user);

        return Results.Created(httpRequest.Path, new UserResponse(user, hasher.EncodeLong(user.Id)));
    }

    public static async Task<IResult> GetUser(
        IIdHasher hasher,
        IUserService userService,
        [FromRoute] string id)
    {
        var longId = hasher.DecodeLong(id);
        if (!longId.HasValue)
        {
            return Results.UnprocessableEntity(new ErrorResponse("USER_NOT_FOUND", "User not found."));
        }

        var user = await userService.GetUser(longId.Value);
        if (user is null)
        {
            return Results.UnprocessableEntity(new ErrorResponse("USER_NOT_FOUND", "User not found."));
        }

        return Results.Ok(new UserResponse(user, hasher.EncodeLong(user.Id)));
    }

    public static async Task<IResult> ListUsers(
        IIdHasher hasher,
        IUserService userService,
        [FromQuery] int offset)
    {
        var total = await userService.CountTotalUsers();
        var user = await userService.ListUsers(offset);

        return Results.Ok(new
        {
            Count = total,
            Users = user.Select(u => new UserResponse(u, hasher.EncodeLong(u.Id))).ToList()
        });
    }

    public static async Task<IResult> Login(
        IIdHasher hasher,
        IUserService userService,
        ITokenGenerator jwtService,
        [FromBody] LoginRequest login)
    {
        var user = await userService.Login(login.Login, login.Password);

        if (user is null)
        {
            return Results.UnprocessableEntity(new ErrorResponse("LOGIN_FAILED", "Invalid login or password."));
        }

        var tokenCredential = new TokenCredential
        {
            Id = hasher.EncodeLong(user.Id),
            Claims = user.Claims?.Select(c => Tuple.Create(c.Type, c.Value)).ToList()
        };

        var token = jwtService.GenerateToken(tokenCredential);

        return Results.Ok(new LoginResponse
        {
            ExpiresAt = token.ExpiresAt,
            Token = token.Value,
            UserId = hasher.EncodeLong(user.Id)
        });
    }
}
