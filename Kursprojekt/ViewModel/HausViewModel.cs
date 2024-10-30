using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace Kursprojekt.ViewModel;

public partial class HausViewModel : BaseViewModel
{
    public HausViewModel()
    {
        Titel = "Haus";
    }

    [RelayCommand]
    public  void GetInitialData()
    {
       
    }
}
