using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    public HomeViewModel()
    {
        Titel = "Home";
    }

    public override void GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            //Datenbankabfrage
        }

        IsViewModelLoaded = true;
        base.GetInitialData();
    }
}
