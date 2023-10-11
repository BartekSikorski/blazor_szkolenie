using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class UsersHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
}
