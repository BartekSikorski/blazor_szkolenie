using BlazorClientApp;
using BlazorClientApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Fluxor;
using Bogus;
using Domain.Models;
using Infrastructure.Fakers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// PM> Install-Package Microsoft.Extensions.Http
builder.Services.AddHttpClient<IssueApiService>(sp => sp.BaseAddress = new Uri("https://localhost:7228"));
builder.Services.AddHttpClient<UserApiService>(sp => sp.BaseAddress = new Uri("https://localhost:7228"));

builder.Services.AddSingleton<HubConnection>(_ => new HubConnectionBuilder()
    .WithUrl("https://localhost:7228/signalr/users")
    .WithAutomaticReconnect()    
    .Build());

// PM> Install-Package Fluxor.Blazor.Web
// PM> Install-Package Fluxor.Blazor.Web.ReduxDevTools
builder.Services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly).UseReduxDevTools());

builder.Services.AddSingleton<Faker<User>, UserFaker>();

// Blazorators: Blazor C# Source Generators
// https://github.com/IEvangelist/blazorators

// https://helion.pl/ksiazki/poznaj-blazor-buduj-jednostronicowe-aplikacje-przy-pomocy-webassembly-i-c-david-pine,e_37h0.htm#format/e

await builder.Build().RunAsync();
