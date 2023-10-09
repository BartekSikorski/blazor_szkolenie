using Domain.Abstractions;
using Domain.Models;

namespace Infrastructure;

public class FakeEntityRepository<T> : IEntityRepository<T>
    where T : BaseEntity
{
    private readonly IDictionary<int, T> _entities;

    public FakeEntityRepository(IEnumerable<T> entities)
    {
        _entities = entities.ToDictionary(p=>p.Id);
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult(_entities.Values.AsEnumerable());
    }

    public Task<T> GetById(int id)
    {
        return Task.FromResult(_entities[id]);
    }
}
