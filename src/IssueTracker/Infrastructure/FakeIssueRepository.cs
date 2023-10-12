using Domain.Abstractions;
using Domain.Models;

namespace Infrastructure;

public class FakeIssueRepository : IIssueRepository
{
    private readonly IDictionary<int, Issue> _issues;

    public FakeIssueRepository(IEnumerable<Issue> issues)
    {
        _issues = issues.ToDictionary(p => p.Id);
    }

    public Task AddAsync(Issue entity)
    {
        throw new NotImplementedException();
    }

    public void Close(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Issue>> GetAllAsync()
    {
        return Task.FromResult(_issues.Values.AsEnumerable());
    }

    public Task<IEnumerable<Issue>> GetAllAsync(IssueParameters parameters)
    {
        var q = _issues.Values.Skip(parameters.StartIndex).Take(parameters.Count).AsEnumerable();

        return Task.FromResult(q);
    }

    public Task<Issue> GetById(int id)
    {
        return Task.FromResult(_issues[id]);
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Issue entity)
    {
        throw new NotImplementedException();
    }
}
