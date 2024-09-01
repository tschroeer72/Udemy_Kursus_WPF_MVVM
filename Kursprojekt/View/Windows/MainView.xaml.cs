using Kursprojekt.View.Pages;
using Kursprojekt.ViewModel;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace Kursprojekt.View.Windows;

/// <summary>
/// Interaktionslogik für MainView.xaml
/// </summary>
public partial class MainView : MetroWindow
{
    public MainViewModel MainViewModel { get; }
    public ServiceProvider? ServiceProvider;
    public MainView(MainViewModel mainViewModel)
    {
        InitializeComponent();
        MainViewModel = mainViewModel;
        DataContext = MainViewModel;
        _InitEvents();
    }

    private void _InitEvents()
    {
        MainViewModel.EventOpenPage += (s, e) => OpenPage(s);
    }

    private void OpenPage(object? oSender)
    {
        if(oSender is Button objButton)
        {
            //if(objButton == BtnHome)
            //{

            //}
            //else if(objButton == BtnAdmin)
            //{

            //}

            switch (objButton.Name)
            {
                case "BtnHome":
                    var pageHome = ServiceProvider?.GetService<HomeView>();
                    if (pageHome == null) return;

                    PagePlace.Children.Clear();
                    PagePlace.Children.Add(pageHome);
                    break;
                case "BtnAdmin":
                    var pageAdmin = ServiceProvider?.GetService<AdminView>();
                    if (pageAdmin == null) return;

                    PagePlace.Children.Clear();
                    PagePlace.Children.Add(pageAdmin);
                    break;
            }
            tpItem.IsSelected = false;
            tpItem.IsSelected = true;
        }
    }
}
