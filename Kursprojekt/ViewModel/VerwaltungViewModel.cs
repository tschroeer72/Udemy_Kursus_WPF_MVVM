using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.ViewModel;

public partial class VerwaltungViewModel : BaseViewModel
{
    public VerwaltungViewModel()
    {
        Titel = "Verwaltung";
    }

    public override void GetInitialData()
    {
        base.GetInitialData();
    }
}
