using Auth.Api.Domain;
using Auth.Api.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;

namespace Auth.Api.Infrastucture;

public class AuthService : IAuthService
{
    private readonly IUserIdentityRepository userIdentityRepository;
    private readonly IPasswordHasher<UserIdentity> passwordHasher;

    public AuthService(IUserIdentityRepository userIdentityRepository, IPasswordHasher<UserIdentity> passwordHasher)
    {
        this.userIdentityRepository = userIdentityRepository;
        this.passwordHasher = passwordHasher;
    }

    public bool TryAuthorize(string username, string password, out UserIdentity identity)
    {
        identity = userIdentityRepository.GetByUsername(username);

        return identity != null && passwordHasher.VerifyHashedPassword(identity, identity.HashedPassword, password) != PasswordVerificationResult.Failed;

        
    }
}
