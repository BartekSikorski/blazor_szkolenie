using BlazorClientApp;
using BlazorClientApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Fluxor;
using Bogus;
using Domain.Models;
using Infrastructure.Fakers;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorClientApp.Authentication;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// PM> Install-Package Microsoft.Extensions.Http
builder.Services.AddHttpClient<IssueApiService>(sp => sp.BaseAddress = new Uri("https://localhost:7228"));
builder.Services.AddHttpClient<UserApiService>(sp => sp.BaseAddress = new Uri("https://localhost:7228"));
builder.Services.AddHttpClient<AuthApiService>(sp => sp.BaseAddress = new Uri("https://localhost:7270"));

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


builder.Services.AddScoped<LazyAssemblyLoader>();


builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("IsAdult", Policies.IsAdultPolicy());
});
builder.Services.AddScoped<CustomAuthenticationStateProvider>();

builder.Services.AddScoped<IAuthentication>(sp=>sp.GetRequiredService<CustomAuthenticationStateProvider>());

builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeAuthorizationHandler>();    


await builder.Build().RunAsync();
