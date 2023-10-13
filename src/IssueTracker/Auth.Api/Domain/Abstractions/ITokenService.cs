namespace Auth.Api.Domain.Abstractions;

// hint:
// SignInResult
// IdentityResult

public interface ITokenService
{
    string Create(UserIdentity identity);
}
