namespace Auth.Api.Domain;

public class UserIdentity
{
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
