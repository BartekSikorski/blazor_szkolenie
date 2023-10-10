﻿using BlazorClientApp.Services;
using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorClientApp.Pages.Users;

public partial class List : IDisposable
{
    private IEnumerable<User> users;

    [Inject]
    public UserApiService Api { get; set; }

    public void Dispose()
    {
        
    }

    protected override async Task OnInitializedAsync()
    {
        users = await Api.GetAllAsync();
    }
}