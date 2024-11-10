using FluentValidation;
using Kursprojekt.Datenbank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.Validators;

public class HausAddAndUpdateValidator : AbstractValidator<Haus>
{
    public HausAddAndUpdateValidator()
    {
        RuleFor(h => h.Bild).NotNull().WithMessage("Das Bild muss hinzugefügt werden.");
        RuleFor(h => h.AuftragID).NotNull().NotEmpty();
        RuleFor(h => h.ZimmerAnzahl).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(h => h.Anschrift).NotNull().NotEmpty();
    }
}