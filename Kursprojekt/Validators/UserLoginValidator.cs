using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Kursprojekt.Datenbank.Models;

namespace Kursprojekt.Validators;

public class UserLoginValidator : AbstractValidator<AppUser>
{
    public UserLoginValidator()
    {
        RuleFor(u => u.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Bitte geben Sie eine gültige EmailAddress ein!");
        RuleFor(u => u.Password).NotNull().NotEmpty();
    }
}