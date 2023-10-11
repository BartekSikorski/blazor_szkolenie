using BlazorClientApp.Services;
using Bogus;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using System.Threading;

namespace BlazorClientApp.Pages.Users;

public partial class List : IDisposable
{
    private IEnumerable<User> users;

    private Timer timer;

    [Inject]
    public UserApiService Api { get; set; }

    public void Dispose()
    {
        timer.Dispose();
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

    

       

    }
}
