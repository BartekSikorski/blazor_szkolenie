namespace Auth.Api.Domain.Abstractions;

public interface IUserIdentityRepository
{
    UserIdentity GetByUsername(string username);
}
