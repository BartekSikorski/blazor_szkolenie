using Domain.Models;

namespace Domain.Abstractions;

public interface IClientUsersHub
{
    Task UserAdded(User user);
}
