using Kursprojekt.ViewModel;
using MahApps.Metro.Controls;
using System.Windows;

namespace Kursprojekt.View.Windows;

/// <summary>
/// Interaktionslogik für MainView.xaml
/// </summary>
public partial class MainView : MetroWindow
{
    public MainViewModel MainViewModel { get; }

    public MainView(MainViewModel mainViewModel)
    {
        InitializeComponent();
        MainViewModel = mainViewModel;
        DataContext = MainViewModel;
    }

}
