using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace Kursprojekt.ViewModel;

public partial class HomeViewModel : BaseViewModel
{
    public HomeViewModel()
    {
        Titel = "Home";
    }

    [RelayCommand]
    public  void GetInitialData()
    {
        if (!IsViewModelLoaded)
        {
            //Datenbankabfrage
        }

        IsViewModelLoaded = true;
        
    }
}
