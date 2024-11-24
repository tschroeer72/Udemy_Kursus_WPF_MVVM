using Kursprojekt.View.Services;
using Kursprojekt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kursprojekt.View.Pages;

/// <summary>
/// Interaktionslogik für HomeView.xaml
/// </summary>
public partial class HomeView : UserControl
{
    public HomeViewModel HomeViewModel { get; }

    public HomeView(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        HomeViewModel = homeViewModel;
        DataContext = HomeViewModel;
        HomeViewModel.InitBaseViewModelDelegateAndEvents();

        HomeViewModel.EventScrollToSelectedHaus += (s, e) => ScrollToSelectedHaus(s);
    }

    private void ScrollToSelectedHaus(object? sender)
    {
        if (sender is int index)
        {
            if (index < 0 && LstBxPerson.Items.Count < index) return;

            LstBxPerson.ScrollIntoView(LstBxPerson.Items[index]);
        }
    }
}