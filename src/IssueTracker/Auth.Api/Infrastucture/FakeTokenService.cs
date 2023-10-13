using Auth.Api.Domain;
using Auth.Api.Domain.Abstractions;

namespace Auth.Api.Infrastucture;

public class FakeTokenService : ITokenService
{
    public string Create(UserIdentity identity)
    {
        return "abc123";
    }
}
