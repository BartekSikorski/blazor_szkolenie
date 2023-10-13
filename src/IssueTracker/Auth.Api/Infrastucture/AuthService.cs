using Auth.Api.Domain;
using Auth.Api.Domain.Abstractions;
using System.Runtime.InteropServices;

namespace Auth.Api.Infrastucture;

public class AuthService : IAuthService
{
    private readonly IUserIdentityRepository userIdentityRepository;

    public AuthService(IUserIdentityRepository userIdentityRepository)
    {
        this.userIdentityRepository = userIdentityRepository;
    }

    public bool TryAuthorize(string username, string password, out UserIdentity identity)
    {
        identity = userIdentityRepository.GetByUsername(username);

        // TODO: porównywać zahaszowane hasło
        return identity != null && identity.HashedPassword == password;

        
    }
}
