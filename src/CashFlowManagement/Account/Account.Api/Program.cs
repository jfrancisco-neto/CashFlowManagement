using Account.Api.Middleware;
using Account.Extensions;
using Account.IOC.Extensions;
using Account.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddAllServices(builder.Configuration);

var app = builder.Build();

app.Use(ExceptionHandler.Handle);

app.MapPost("user", DefaultRoute.CreateUser).RequireAuthorization("CreateUserPolicy");
app.MapGet("user/{id}", DefaultRoute.GetUser);
app.MapGet("user", DefaultRoute.ListUsers);
app.MapPost("auth", DefaultRoute.Login).AllowAnonymous();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
