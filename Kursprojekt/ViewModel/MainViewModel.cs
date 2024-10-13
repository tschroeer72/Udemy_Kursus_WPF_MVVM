using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Kursprojekt.ViewModel;

public partial class MainViewModel : BaseViewModel  
{

    public MainViewModel()
    {
        Titel = "Main";
    }

    [RelayCommand]
    public void ShowLoginView()
    {
        DelShowLoginView?.Invoke(true);
    }
}
