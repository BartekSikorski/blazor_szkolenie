using Domain.Models;
using System.Net.Http.Json;

namespace BlazorClientApp.Services;

public class UserApiService
{
    private readonly HttpClient client;
    public UserApiService(HttpClient client) => this.client = client;
    public Task<IEnumerable<User>> GetAllAsync() => client.GetFromJsonAsync<IEnumerable<User>>("api/users");
    public Task<IEnumerable<User>> GetBySearchCriteriaAsync(UserSearchCriteria criteria) => client.GetFromJsonAsync<IEnumerable<User>>($"/api/users/search?{criteria}");
    public Task AddAsync(User user) => client.PostAsJsonAsync("api/users", user);

}