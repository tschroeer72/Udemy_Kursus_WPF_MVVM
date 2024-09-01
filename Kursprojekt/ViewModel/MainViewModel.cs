using CommunityToolkit.Mvvm.ComponentModel;

namespace Kursprojekt.ViewModel;

[INotifyPropertyChanged] //(Alternative 2)
public partial class MainViewModel //: ObservableObject (Alternative 1)
{
    public MainViewModel()
    {
        Titel = "MainView";
    }

    [ObservableProperty]
    string titel = "";
}
