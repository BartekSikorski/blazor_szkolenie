using Domain.Models;

namespace Domain.Abstractions;

public interface IIssueRepository : IEntityRepository<Issue>
{
    Task<IEnumerable<Issue>> GetAllAsync(IssueParameters parameters);
    void Close(int id);
}
