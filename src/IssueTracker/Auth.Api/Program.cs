using Auth.Api.Domain;
using Auth.Api.Domain.Abstractions;
using Auth.Api.Infrastucture;
using Auth.Api.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPasswordHasher<UserIdentity>, PasswordHasher<UserIdentity>>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddSingleton<IUserIdentityRepository, FakeUserIdentityRepository>();

builder.Services.AddSingleton<IEnumerable<UserIdentity>>(sp =>
{
    var passwordHasher = sp.GetRequiredService<IPasswordHasher<UserIdentity>>();

    var userIdentities = new List<UserIdentity>()
    {
        new UserIdentity { Username = "john", HashedPassword = "123", Email = "john@domain.com"},
        new UserIdentity { Username = "bob", HashedPassword = "123", Email = "bob@domain.com"},
        new UserIdentity { Username = "kate", HashedPassword = "123", Email = "kate@domain.com"}
    };

    foreach (var userIdentity in userIdentities)
    {
        userIdentity.HashedPassword = passwordHasher.HashPassword(userIdentity, userIdentity.HashedPassword);
    }

    return userIdentities;
});


builder.Services.Configure<JwtTokenServiceOptions>(builder.Configuration.GetSection("JWTTokens"));

var app = builder.Build();

app.MapGet("/", () => "Hello Auth Api!");

// POST /api/token/create
//  { "username":"john", "password":"your-password"}
// 
// 

app.MapPost("/api/token/create", (LoginModel model,
    IAuthService authService, ITokenService tokenService) =>
{
    if (authService.TryAuthorize(model.Username, model.Password, out var identity))
    {
        string token = tokenService.Create(identity);
        return Results.Ok(token);
    }

    return Results.BadRequest(new { messsage = "Invalid username or password" });


});


string secretKey = app.Configuration["JWTTokens:SecretKey"];

string googleSecretKey = app.Configuration["GoogleMapsKey"];

app.Run();
