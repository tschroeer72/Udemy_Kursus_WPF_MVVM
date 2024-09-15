using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.ViewModel;

public partial class LoginViewModel:BaseViewModel
{
   

    [RelayCommand]
    void LoginUser()
    {
        //User in DB suchen

        //WENN gefunden DANN HomeView öffnen
        DelGoBackOrGotoHome?.Invoke();
    }
}
