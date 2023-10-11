using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Note { get; set; }
    public bool IsSelected { get; set; }
    public bool IsRemoved { get; set; }

    public ICollection<Address> Addresses { get; set; } = new List<Address>();

    public void Add(Address address)
    {
        Addresses.Add(address);
    }

    public void Remove(Address address)
    {
        Addresses.Remove(address);
    }
}


public class Address
{
    public string City { get; set; }
    public string Street { get; set; }
}