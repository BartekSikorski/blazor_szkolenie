namespace Domain.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Note { get; set; }
    public bool IsSelected { get; set; }
    public bool IsRemoved { get; set; }
}