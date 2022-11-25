using Account.Domain.Exceptions;
using Account.Options;
using Account.Persistence;
using Account.Repository;
using Account.Response;
using Account.Routes;
using Account.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<PersistenceContext>(b => {
    b.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IIdHasher, IdHasher>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddSingleton<AuthOptions>(sp => sp.GetRequiredService<IOptions<AuthOptions>>().Value);
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));
builder.Services.AddSingleton<HashIdOptions>(sp => sp.GetRequiredService<IOptions<HashIdOptions>>().Value);
builder.Services.Configure<HashIdOptions>(builder.Configuration.GetSection("HashId"));

var app = builder.Build();

app.Use(async (context, next) => 
{
    try
    {
        await next(context);
    }
    catch(DomainException e)
    {
        context.Response.StatusCode = (int) System.Net.HttpStatusCode.UnprocessableEntity;

        await context.Response.WriteAsJsonAsync(new ErrorResponse(e.Message));
    }
});

app.MapPost("user", DefaultRoute.CreateUser);
app.MapGet("user/{id}", DefaultRoute.GetUser);
app.MapGet("user", DefaultRoute.ListUsers);
app.MapPost("auth", DefaultRoute.Login);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
