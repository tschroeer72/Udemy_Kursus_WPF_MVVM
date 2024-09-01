using Kursprojekt.ViewModel;
using System.Windows;

namespace Kursprojekt.View.Windows;

/// <summary>
/// Interaktionslogik für MainView.xaml
/// </summary>
public partial class MainView : Window
{
    public MainViewModel MainViewModel { get; }

    public MainView(MainViewModel mainViewModel)
    {
        InitializeComponent();
        MainViewModel = mainViewModel;
        DataContext = MainViewModel;
    }

}
