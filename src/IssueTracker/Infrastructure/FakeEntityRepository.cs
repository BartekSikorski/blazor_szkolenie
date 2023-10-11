using Domain.Abstractions;
using Domain.Models;

namespace Infrastructure;

public class FakeEntityRepository<T> : IEntityRepository<T>
    where T : BaseEntity
{
    protected readonly IDictionary<int, T> _entities;

    public FakeEntityRepository(IEnumerable<T> entities)
    {
        _entities = entities.ToDictionary(p=>p.Id);
    }

    public Task AddAsync(T entity)
    {
        int id = _entities.Max(e => e.Key);
        entity.Id = ++id;
        _entities.Add(entity.Id, entity);

        return Task.CompletedTask;
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult(_entities.Values.AsEnumerable());
    }

    public Task<T> GetById(int id)
    {
        return Task.FromResult(_entities[id]);
    }

    public async Task RemoveAsync(int id)
    {
        _entities.Remove(id);        
    }

    public async Task UpdateAsync(T entity)
    {
        _entities[entity.Id] = entity;        
    }
}
