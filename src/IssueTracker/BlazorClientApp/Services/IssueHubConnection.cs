using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Net;

namespace BlazorClientApp.Services
{
    public class IssueHubConnection : HubConnection
    {
        public IssueHubConnection(IConnectionFactory connectionFactory, IHubProtocol protocol, EndPoint endPoint, IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IRetryPolicy reconnectPolicy) : base(connectionFactory, protocol, 
            new UriEndPoint(new Uri("https://localhost:7228/signalr/issues")), serviceProvider, loggerFactory, reconnectPolicy)
        {
           
        }
    }

  
}
