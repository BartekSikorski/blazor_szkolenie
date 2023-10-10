using Bogus;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading;

namespace Api.Hubs;

public class IssuesHub : Hub
{
    private readonly ILogger<IssuesHub> logger;

    public IssuesHub(ILogger<IssuesHub> logger)
    {
        this.logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        // zła praktyka!
        // logger.LogInformation($"Connected ConnectionId: {Context.ConnectionId}");

        // dobra praktyka
        logger.LogInformation("Connected ConnectionId: {ConnectionId}", Context.ConnectionId);

        var faker = new Faker<Issue>()
            .RuleFor(p => p.Title, f => f.Hacker.Verb())
            .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
            ;

       
        await base.OnConnectedAsync();

        var issues = faker.GenerateForever();

        foreach(var issue in issues)
        {
            await this.Clients.All.SendAsync("IssueAdded", issue);
            await Task.Delay(1000);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation("Disconnected ConnectionId: {ConnectionId}", Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}
