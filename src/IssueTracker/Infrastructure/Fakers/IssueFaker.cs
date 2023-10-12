using Bogus;
using Domain.Models;

namespace Infrastructure.Fakers;

public class IssueFaker : Faker<Issue>
{
    public IssueFaker()
    {
        RuleFor(p => p.Id, f => f.IndexFaker);
        RuleFor(p=>p.Title, f=>f.Hacker.Verb());
        RuleFor(p => p.Description, f => f.Lorem.Paragraph());
        RuleFor(p => p.Status, f => f.PickRandom<Status>());
    }
}
