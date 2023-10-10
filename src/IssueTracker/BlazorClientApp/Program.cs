using BlazorClientApp;
using BlazorClientApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// PM> Install-Package Microsoft.Extensions.Http
builder.Services.AddHttpClient<IssueApiService>(sp => sp.BaseAddress = new Uri("https://localhost:7228"));
builder.Services.AddHttpClient<UserApiService>(sp => sp.BaseAddress = new Uri("https://localhost:7228"));


builder.Services.AddSingleton<HubConnection>(_ => new HubConnectionBuilder()
    .WithUrl("https://localhost:7228/signalr/issues")
    .WithAutomaticReconnect()
    .Build());

await builder.Build().RunAsync();
