using Api.Hubs;
using Bogus;
using Domain.Abstractions;
using Domain.Models;
using Infrastructure;
using Infrastructure.Fakers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Faker<User>, UserFaker>();
builder.Services.AddSingleton<Faker<Issue>, IssueFaker>();
builder.Services.AddSingleton<IIssueRepository, FakeIssueRepository>();
builder.Services.AddSingleton<IUserRepository, FakeUserRepository>();
builder.Services.AddSingleton<IEnumerable<User>>(sp =>
{
    var faker = sp.GetRequiredService<Faker<User>>();

    var users = faker.Generate(5);

    return users;
});

builder.Services.AddSingleton<IEnumerable<Issue>>(sp =>
{
    var faker = sp.GetRequiredService<Faker<Issue>>();

    var issues = faker.Generate(10_000);

    return issues;
});

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


builder.Services.AddSignalR();

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

app.MapGet("/api/users/{id}", async (IUserRepository repository, int id) => await repository.GetById(id))
    .WithName("GetUserById");

app.MapPost("/api/users", async (IUserRepository repository, [FromBody] User user, IHubContext<StrongTypedUsersHub, IClientUsersHub> hubContext) => {
    
    await repository.AddAsync(user);

    // await hubContext.Clients.All.SendAsync("UserAdded", user);

    await hubContext.Clients.All.UserAdded(user);

    return Results.CreatedAtRoute("GetUserById", new { id = user.Id }, user);
});

app.MapPut("/api/users/{id}", async (IUserRepository repository, int id, User user) => await repository.UpdateAsync(user));

/* 
 REST API - RESTfull

 GET  /api/users/{id}      - pobierz
 POST /api/users           - utwórz
 PUT /api/users/{id}       - zamieñ
 PATCH /api/users/{id}     - zmieñ
 DELETE  /api/users/{id}   - usuñ

 // OpenApi / Swagger

 request
 --- 
 GET /api/users HTTP/1.1
 Accept: application/json
 X-Version: 2.0
 ---

 response
 ---
 200 OK 
 ---

 request
 --- 
 POST /api/users HTTP/1.1
 Content-Type: application/json
 {
    "FirstName":"John",
    "LastName":"Smith",
 }
 --- 

 response
 ---
 201 Created
 Content-Type: application/json
 Location: /api/users/10
 {
    "Id": 10,
    "FirstName":"John",
    "LastName":"Smith",
 }
 ---


 request
 --- 
 PUT /api/users/10 HTTP/1.1
 Content-Type: application/json
 {
    "FirstName":"Bob",
    "LastName":"Smith",
 }
 --- 

 response
 ---
 204 NoContent
 ---
 
*/


app.MapHub<IssuesHub>("signalr/issues");
app.MapHub<StrongTypedUsersHub>("signalr/users");

app.Run();


