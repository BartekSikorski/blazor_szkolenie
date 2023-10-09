using Domain.Models;
using System.Net.Http.Json;

namespace BlazorClientApp.Services;

public class IssueApiService
{
    private readonly HttpClient client;

    public IssueApiService(HttpClient client)
    {
        this.client = client;
    }

    public Task<IEnumerable<Issue>> GetAllAsync() => client.GetFromJsonAsync<IEnumerable<Issue>>("api/issues");
    public Task<Issue> GetByIdAsync(int id) => client.GetFromJsonAsync<Issue>($"api/issues/{id}");
}
