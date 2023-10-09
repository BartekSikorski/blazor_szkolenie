using Domain.Models;
using System.Net.Http.Json;

namespace BlazorClientApp.Services;

public class UserApiService
{
    private readonly HttpClient client;
    public UserApiService(HttpClient client) => this.client = client;
    public Task<IEnumerable<User>> GetAllAsync() => client.GetFromJsonAsync<IEnumerable<User>>("api/users");

}