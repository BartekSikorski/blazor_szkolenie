using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public abstract class SearchCriteria
{

}

public class UserSearchCriteria : SearchCriteria
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public override string ToString()
    {
        return $"FirstName={FirstName}&LastName={LastName}";
    }
}
