using Account.Requests;
using Account.Response;
using Account.Service;
using Microsoft.AspNetCore.Mvc;

namespace Account.Routes;

public static class DefaultRoute
{
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
        var user = await userService.GetUser(hasher.DecodeLong(id));

        if (user is null)
        {
            return Results.UnprocessableEntity(new ErrorResponse("User not found."));
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
        [FromBody] LoginRequest login)
    {
        var user = await userService.Login(login.Login, login.Password);

        if (user is null)
        {
            return Results.UnprocessableEntity(new ErrorResponse("Invalid login or password."));
        }

        return Results.Ok(new UserResponse(user, hasher.EncodeLong(user.Id)));
    }
}
