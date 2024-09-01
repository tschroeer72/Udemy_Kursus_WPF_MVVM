using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Kursprojekt.ViewModel;

public partial class MainViewModel : BaseViewModel  
{
    public event EventHandler? EventOpenPage;

    public MainViewModel()
    {
        Titel = "Main";
    }

    [RelayCommand]
    void OpenPage(object oParam)
    {
        EventOpenPage?.Invoke(oParam, EventArgs.Empty);
    }
}
