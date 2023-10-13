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
        new UserIdentity { Username = "john", HashedPassword = "123", Email = "john@domain.com", Birthday = DateTime.Today.AddYears(-19)},
        new UserIdentity { Username = "bob", HashedPassword = "123", Email = "bob@domain.com", Role = "administrator", Birthday = DateTime.Today.AddYears(-17) },
        new UserIdentity { Username = "kate", HashedPassword = "123", Email = "kate@domain.com", Role = "developer" }
    };

    foreach (var userIdentity in userIdentities)
    {
        userIdentity.HashedPassword = passwordHasher.HashPassword(userIdentity, userIdentity.HashedPassword);
    }

    return userIdentities;
});


if (builder.Environment.IsDevelopment())
{
    // Rejestracja reguly CORS
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            // policy.AllowAnyOrigin();
            policy.WithOrigins("https://localhost:7085", "http://localhost:5281");
            policy.WithMethods("GET", "POST", "PUT");
            policy.AllowAnyHeader();
        });
    });
}

builder.Services.Configure<JwtTokenServiceOptions>(builder.Configuration.GetSection("JWTTokens"));

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseCors();
}

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

    return Results.Problem("Invalid username or password", title: "Authorization failed");

});


string secretKey = app.Configuration["JWTTokens:SecretKey"];

string googleSecretKey = app.Configuration["GoogleMapsKey"];

app.Run();
