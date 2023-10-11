using Domain.Models;

public interface IClientUsersHub
{
    Task UserAdded(User user);
}
