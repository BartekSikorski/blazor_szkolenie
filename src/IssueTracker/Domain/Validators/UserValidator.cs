using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators;

// PM> Install-Package FluentValidation
public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(p=>p.FirstName).NotEmpty().MinimumLength(3).MaximumLength(20);
        RuleFor(p => p.LastName).NotEmpty();
        RuleFor(p => p.Email).EmailAddress();
        RuleForEach(p => p.Addresses).SetValidator(new AddressValidator());
        RuleFor(p => p.Addresses).Must(a => a.Any())
            .WithErrorCode("199")
            .WithMessage("Musisz podać przynajmniej jeden adres.");
    }
}


public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(p=>p.City).NotEmpty();
        RuleFor(p=>p.Street).NotEmpty();
    }
}
