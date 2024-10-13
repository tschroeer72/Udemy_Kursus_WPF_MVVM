using FluentValidation;
using Kursprojekt.Datenbank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.Validators;

public class UserModelValidator : AbstractValidator<AppUser>
{

    public UserModelValidator()
    {
        RuleFor(u => u.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(u => u.Password).NotNull().NotEmpty();
        RuleFor(u => u.RoleID).NotNull().NotEmpty().GreaterThan(0);
    }
}