using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.ViewModel;

public partial class HausViewModel : BaseViewModel
{
    public HausViewModel()
    {
        Titel = "Haus";
    }

    public override void GetInitialData()
    {
        base.GetInitialData();
    }
}
