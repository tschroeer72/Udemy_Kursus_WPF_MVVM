using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.ViewModel;

public partial class BuchungViewModel :BaseViewModel
{
    public BuchungViewModel()
    {
        Titel = "Buchung";
    }

    public override void GetInitialData()
    {
        base.GetInitialData();
    }
}
