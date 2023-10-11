using Domain.Models;

namespace Domain.Abstractions;

public interface IUserRepository : IEntityRepository<User>
{
    Task<IEnumerable<User>> GetByNameAsync(string name);
    Task<IEnumerable<User>> GetBySearchCriteriaAsync(UserSearchCriteria criteria);
   
}