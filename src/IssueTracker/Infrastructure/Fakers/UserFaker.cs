using Bogus;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Fakers;


// PM> Install-Package Bogus
public class UserFaker : Faker<User>
{
    public UserFaker()
    {
        UseSeed(1);
        StrictMode(true);
        RuleFor(p => p.Id, f => f.IndexFaker);
        RuleFor(p => p.FirstName, f => f.Person.FirstName);
        RuleFor(p => p.LastName, f => f.Person.LastName);
        RuleFor(p => p.Email, (f,user) => $"{user.FirstName}.{user.LastName}@domain.com");
        RuleFor(p => p.Note, f => f.Lorem.Paragraph());
        Ignore(p => p.IsSelected);
        RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.3f));
        RuleFor(p => p.CreatedAt, f => f.Date.Past());
        RuleFor(p => p.UpdatedAt, (f, user) => f.Date.Between(user.CreatedAt, DateTime.Now));
        Ignore(p => p.Addresses);
    }
}
