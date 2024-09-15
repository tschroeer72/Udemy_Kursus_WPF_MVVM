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

        //Test des InfoWindows und des Flyouts
        //var erg1 = DelShowInformationWindow?.Invoke("Hallo 1");
        //var erg2 = DelShowConfirmationWindow?.Invoke("Hallo 2");
        //var erg3 = DelShowInputWindow?.Invoke("Hallo 3");
        //DelShowMainInfoFlyout?.Invoke("Hallo 4");
        //DelShowMainInfoFlyout?.Invoke("Hallo 5", true);

        //User in DB suchen

        //WENN gefunden DANN HomeView öffnen
        DelGoBackOrGotoHome?.Invoke();
    }
}
