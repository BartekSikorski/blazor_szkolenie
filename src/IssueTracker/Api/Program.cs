using Domain.Abstractions;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IIssueRepository, FakeIssueRepository>();
builder.Services.AddSingleton<IUserRepository, FakeUserRepository>();
builder.Services.AddSingleton<IEnumerable<User>>(_ =>
{
    return new List<User>
    {
        new User { Id = 1, FirstName = "John", LastName = "Smith"},
        new User { Id = 2, FirstName = "Bob", LastName = "Smith"},
        new User { Id = 3, FirstName = "Ann", LastName = "Smith"},
    };
});

// Rejestracja reguly CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        // policy.AllowAnyOrigin();
        policy.WithOrigins("https://localhost:7085", "http://localhost:5281");
        policy.WithMethods("GET");
    });
});

var app = builder.Build();

// Wlaczenie CORS
app.UseCors();

// Minimal Api

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/issues", async (IIssueRepository repository) => await repository.GetAllAsync());
app.MapGet("/api/issues/{id:int}", async (IIssueRepository repository, int id) => await repository.GetById(id));

app.MapGet("/api/users", async (IUserRepository repository) => await repository.GetAllAsync());

// GET /api/users/search?name=Bob
// app.MapGet("/api/users/search", async(IUserRepository repository, string name) => await repository.GetByNameAsync(name));

app.MapGet("/api/users/search", async (IUserRepository repository, [AsParameters] UserSearchCriteria criteria) => await repository.GetBySearchCriteriaAsync(criteria));

app.Run();
