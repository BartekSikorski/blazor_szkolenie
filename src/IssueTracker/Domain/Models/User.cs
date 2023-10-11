using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class User : BaseEntity
{
    [Required, StringLength(10, MinimumLength = 3)]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Note { get; set; }
    public bool IsSelected { get; set; }
    public bool IsRemoved { get; set; }
}