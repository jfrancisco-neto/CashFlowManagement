using System.Text;
using Account.Domain.Exceptions;
using Account.Extensions;
using Account.Options;
using Account.Persistence;
using Account.Repository;
using Account.Response;
using Account.Routes;
using Account.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
// {
//     var jwtOptions = new JwtOptions();
//     builder.Configuration.GetSection("JWT").Bind(jwtOptions);

//     opt.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience =  true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidAudience = jwtOptions.Audience,
//         ValidIssuer = jwtOptions.Issuer,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
//     };
// });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<PersistenceContext>(b =>
    b.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IIdHasher, IdHasher>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IJwtService, JwtService>();

builder.MapOption<AuthOptions>("Auth");
builder.MapOption<HashIdOptions>("HashId");
builder.MapOption<JwtOptions>("JWT");

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

app.MapPost("user", DefaultRoute.CreateUser);
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
