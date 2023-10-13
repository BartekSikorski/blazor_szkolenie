using BlazorClientApp.Models;
using BlazorClientApp.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorClientApp.Authentication;

public interface IAuthentication
{
    Task LoginAsync(LoginModel model);
    Task LogoutAsync();
}

// PM> Install-Package Blazored.LocalStorage
public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IAuthentication
{
    private readonly AuthApiService Api;
    private readonly ILocalStorageService localStorage;

    private const string AccessTokenKey = "access-token";

    public CustomAuthenticationStateProvider(AuthApiService api, ILocalStorageService localStorage)
    {
        Api = api;
        this.localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var state = new AuthenticationState(new ClaimsPrincipal());
        var token = await localStorage.GetItemAsStringAsync(AccessTokenKey);

        // PM> Install-Package System.IdentityModel.Tokens.Jwt
        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (tokenHandler.CanReadToken(token))
            {
                var jwt = tokenHandler.ReadJwtToken(token);

                var identity = new ClaimsIdentity(jwt.Claims, "JWT Tokens");
                state = new AuthenticationState(new ClaimsPrincipal(identity));
            }
        }

        return state;
    }

    public async Task LoginAsync(LoginModel model)
    {
        try
        {
            var token = await Api.Create(model);

            await localStorage.SetItemAsStringAsync(AccessTokenKey, token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        catch(HttpRequestException e)
        {

        }
    }

    public async Task LogoutAsync()
    {
        await localStorage.RemoveItemAsync(AccessTokenKey);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}

