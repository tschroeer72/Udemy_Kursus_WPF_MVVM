using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Kursprojekt.ViewModel;

[INotifyPropertyChanged] //(Alternative 2)
public partial class MainViewModel //: ObservableObject (Alternative 1)
{
    public MainViewModel()
    {
        Titel = "Main";
        FilterText = "Filter";
    }

    [ObservableProperty]
    string titel = "";

    [ObservableProperty]
    string filterText = "";

    [RelayCommand]
    void ChangeTitel()
    {
        Titel = "MainView";
        var text = FilterText;
    }
}
