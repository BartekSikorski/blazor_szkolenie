using BlazorClientApp.Services;
using Bogus;
using Domain.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading;

namespace BlazorClientApp.Pages.Users;

public partial class List : IAsyncDisposable
{
    private IEnumerable<User> users;

    private Timer timer;

    [Inject]
    public UserApiService Api { get; set; }

    [Inject]
    public HubConnection Connection { get; set; }
    
    public async ValueTask DisposeAsync()
    {
        timer.Dispose();
        await Connection.StopAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        
        var faker = new Faker<User>()
        .RuleFor(p => p.FirstName, f => f.Person.FirstName)
        .RuleFor(p => p.LastName, f => f.Person.LastName);

        timer = new Timer(new TimerCallback(_ =>
        {
            var u = users.ToList();
            u.Add(faker.Generate());
            users = u.AsEnumerable();

            StateHasChanged();

        }), null, 5000, 5000);

        users = await Api.GetAllAsync();

        Connection.On<User>(nameof(IClientUsersHub.UserAdded), user =>
        {
            var u = users.ToList();
            u.Add(user);
            users = u.AsEnumerable();

            StateHasChanged();
        });

        await Connection.StartAsync();




    }
}
