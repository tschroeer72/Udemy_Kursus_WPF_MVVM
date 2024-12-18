﻿using System.Diagnostics.Eventing.Reader;
using Kursprojekt.View.Pages;
using Kursprojekt.View.Services;
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

    public MainView(MainViewModel mainViewModel)
    {
        InitializeComponent();
        MainViewModel = mainViewModel;
        DataContext = MainViewModel;
        MainViewModel.InitBaseViewModelDelegateAndEvents();

        //ViewManager.InitBaseDelEvents(MainViewModel);
    }

    private void OpenPageOnMain(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton objButton)
        {
            switch (objButton.Name)
            {
                case "BtnHome":
                    ViewManager.ShowPageOnMainView<HomeView>();
                    break;
                case "BtnVerwaltung":
                    ViewManager.ShowPageOnMainView<VerwaltungView>(true);
                    break;
                case "BtnAdmin":
                    ViewManager.ShowPageOnMainView<AdminView>();
                    break;
            }
        }
        else if (sender is MenuItem objMenuItem)
        {
            switch (objMenuItem.Name)
            {
                case "MeuBtnLogout":
                    ViewManager.ShowLoginView(true);
                    break;
            }
        }
    }
}
