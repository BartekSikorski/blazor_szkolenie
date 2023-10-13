using Auth.Api.Domain;
using Auth.Api.Domain.Abstractions;

namespace Auth.Api.Infrastucture;

public class FakeUserIdentityRepository : IUserIdentityRepository
{
    private readonly IDictionary<string, UserIdentity> _userIdentities = new Dictionary<string, UserIdentity>();

    public FakeUserIdentityRepository(IEnumerable<UserIdentity> userIdentities)
    {
        _userIdentities = userIdentities.ToDictionary(p=>p.Username);
    }

    public UserIdentity GetByUsername(string username)
    {
        _userIdentities.TryGetValue(username, out var userIdentity);

        return userIdentity;
        
    }
}
