using FluentValidation;
using Kursprojekt.Datenbank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.Validators;

public class AddBuchungValidator : AbstractValidator<Buchung>
{
    public AddBuchungValidator()
    {
        RuleFor(b => b.Bearbeiter).NotNull().NotEmpty();
        RuleFor(b => b.KundenName).NotNull().NotEmpty();
        RuleFor(b => b.KundenTelefonNummer).NotNull().NotEmpty();
        RuleFor(b => b.BesichtigungDatum).NotNull().NotEmpty().GreaterThan(DateTime.Now.AddDays(-1)).WithMessage("Das Besichtigungdatum kann nicht in der Vergangenheit sein!");
        RuleFor(b => b.BesichtigungZeit).NotNull().NotEmpty();
    }
}