using Account.Api.Response;
using Account.Domain.Exceptions;
using Account.Extensions;
using Account.IOC.Extensions;
using Account.Routes;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddAllServices(builder.Configuration);

var app = builder.Build();

app.Use(async (context, next) => 
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

    try
    {
        await next(context);
    }
    catch(Exception e)
    {
        var statusCode = (int) System.Net.HttpStatusCode.InternalServerError;
        var errorMessage = "INTERNAL_SERVER_ERROR";

        if (e is DomainException)
        {
            statusCode = (int) System.Net.HttpStatusCode.UnprocessableEntity;
            errorMessage = e.Message;
        }

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new ErrorResponse(errorMessage));

        logger.LogError(e, "Unhandled exception.");
    }
});

app.MapPost("user", DefaultRoute.CreateUser).AllowAnonymous();
app.MapGet("user/{id}", DefaultRoute.GetUser);
app.MapGet("user", DefaultRoute.ListUsers);
app.MapPost("auth", DefaultRoute.Login).AllowAnonymous();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();

app.Run();
