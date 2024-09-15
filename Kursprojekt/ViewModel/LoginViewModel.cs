using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.ViewModel;

public partial class LoginViewModel:BaseViewModel
{
    public delegate void DelGoBackOrGotoHomeType();
    public DelGoBackOrGotoHomeType? DelBockBackOrGotoHome { get; set; }

    [RelayCommand]
    void LoginUser()
    {
        //User in DB suchen

        //WENN gefunden DANN HomeView öffnen
        DelBockBackOrGotoHome?.Invoke();
    }
}
