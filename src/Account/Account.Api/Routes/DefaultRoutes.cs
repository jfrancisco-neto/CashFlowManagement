using Account.Domain.Service;
using Account.Security.Service;
using Microsoft.AspNetCore.Mvc;
using Account.Api.Requests;
using Account.Api.Response;
using Account.Security.Model;
using Shared.Api.Response;
using FluentValidation;
using Shared.Api.Extensions;

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
        HttpRequest httpRequest,
        IIdHasher hasher,
        IUserService userService,
        IValidator<CreateUserRequest> validator,
        [FromBody] CreateUserRequest request)
    {
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            return Results.UnprocessableEntity(result.ToErrorResponse());
        }

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
        IValidator<LoginRequest> validator,
        [FromBody] LoginRequest login)
    {
        var result = validator.Validate(login);
        if (!result.IsValid)
        {
            return Results.UnprocessableEntity(result.ToErrorResponse());
        }

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
