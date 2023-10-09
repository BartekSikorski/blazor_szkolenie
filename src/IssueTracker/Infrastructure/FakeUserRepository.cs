using Domain.Abstractions;
using Domain.Models;

namespace Infrastructure;

public class FakeUserRepository : FakeEntityRepository<User>, IUserRepository
{
    public FakeUserRepository(IEnumerable<User> entities) : base(entities)
    {
    }
}
