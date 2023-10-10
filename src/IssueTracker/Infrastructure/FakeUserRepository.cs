using Domain.Abstractions;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure;

public class FakeUserRepository : FakeEntityRepository<User>, IUserRepository
{
    public FakeUserRepository(IEnumerable<User> entities) : base(entities)
    {
    }

    public Task<IEnumerable<User>> GetByNameAsync(string name)
    {
        var users = _entities.Values.Where(e => e.FirstName.Contains(name));

        return Task.FromResult(users);
    }

    public Task<IEnumerable<User>> GetBySearchCriteriaAsync(UserSearchCriteria criteria)
    {
        var results = _entities.Values.AsQueryable();

        if (!string.IsNullOrEmpty(criteria.FirstName))
        {
            results = results.Where(e=>e.FirstName.Contains(criteria.FirstName));
        }

        if (!string.IsNullOrEmpty(criteria.LastName))
        {
            results = results.Where(e => e.LastName.Contains(criteria.LastName));
        }

        if (criteria.From.HasValue)
        {
            results = results.Where(e => e.CreatedAt >= criteria.From);
        }

        if (criteria.To.HasValue)
        {
            results = results.Where(e => e.CreatedAt <= criteria.To);
        }

        var users = results.AsEnumerable();

        return Task.FromResult(users);

    }
}
