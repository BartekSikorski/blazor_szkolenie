using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Domain.Abstractions;

public interface IAuthService
{
     bool TryAuthorize(string username, string password, out UserIdentity identity);
}

