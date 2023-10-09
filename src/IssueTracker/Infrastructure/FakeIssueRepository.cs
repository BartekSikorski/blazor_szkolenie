using Domain.Abstractions;
using Domain.Models;

namespace Infrastructure;

public class FakeIssueRepository : IIssueRepository
{
    private readonly IDictionary<int, Issue> _issues;

    public FakeIssueRepository()
    {
        _issues = new List<Issue>
        {
            new Issue { Id = 1, Title = "Issue 1", Description = "Lorem ipsum 1" },
            new Issue { Id = 2, Title = "Issue 2", Description = "Lorem ipsum 2" },
            new Issue { Id = 3, Title = "Issue 3", Description = "Lorem ipsum 3" },
        }
        .ToDictionary(p=>p.Id);
    }

    public void Close(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Issue>> GetAllAsync()
    {
        return Task.FromResult(_issues.Values.AsEnumerable());
    }

    public Task<Issue> GetById(int id)
    {
        return Task.FromResult(_issues[id]);
    }
}