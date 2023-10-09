using Domain.Abstractions;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IIssueRepository, FakeIssueRepository>();

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

app.Run();
