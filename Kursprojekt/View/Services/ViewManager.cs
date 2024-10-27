using Kursprojekt.Enums;
using Kursprojekt.View.Pages;
using Kursprojekt.View.UserControls;
using Kursprojekt.View.Windows;
using Kursprojekt.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace Kursprojekt.View.Services;

public class ViewManager
{
    private static UserControl? GoBackPage { get; set; }
    public static MainView? MainView { get; set; }
    //public static MainViewModel? MainViewModel { get; set; }
    public static ServiceProvider? ServiceProvider { get; set; }

    public static void InitViewManager(MainView mainView, ServiceProvider serviceProvider)
    {
        MainView = mainView;
        //MainViewModel = mainView.MainViewModel;
        ServiceProvider = serviceProvider;
    }

    public static void ShowPageOnMainView<T>(bool bFullPage = false) where T : UserControl
    {
		try
		{
            var pageService = ServiceProvider?.GetService<T>();
            if (pageService == null) return;
            if (pageService is UserControl page) 
            {
                Show(page, bFullPage);
            }
		}
		catch (Exception ex)
		{
            MessageBox.Show(ex.ToString());
		}
    }

    public static void Show(UserControl ucPage, bool bFullPage = false)
    {
        MainView!.AnimatedContentControl.PagePlace.Content = null;
        MainView!.AnimatedContentControl.PagePlace.Content = ucPage;

        MainView!.AnimatedContentControl.MetroTabItem.IsSelected = false;
        MainView!.AnimatedContentControl.MetroTabItem.IsSelected = true;

        if (bFullPage) 
        {
            MainView!.TitleBar.Visibility = Visibility.Collapsed;
        }
        else
        {
            MainView!.TitleBar.Visibility=Visibility.Visible;
            GoBackPage = ucPage;
        }

        ActivateViwModel(ucPage);
        DoChangeOnViewAfterShow(ucPage);
    }

    public static void DoChangeOnViewAfterShow(UserControl ucPage)
    {
        MainView!.UserPanel.Visibility = Visibility.Visible;

        MainView!.BtnHome.IsChecked = ucPage is HomeView;
        MainView!.BtnVerwaltung.IsChecked = ucPage is VerwaltungView;
        MainView!.BtnAdmin.IsChecked = ucPage is AdminView;

        MainView!.ShowCloseButton = true;
        //switch (ucPage.Name)
        //{
        //    case "HomeView":
        //        MainView!.BtnHome.IsChecked = true;
        //        MainView!.BtnVerwaltung.IsChecked = false;
        //        MainView!.BtnAdmin.IsChecked = false;
        //        break;
        //    case "VerwaltungView":
        //        MainView!.BtnHome.IsChecked = false;
        //        MainView!.BtnVerwaltung.IsChecked = true;
        //        MainView!.BtnAdmin.IsChecked = false;
        //        break;
        //    case "AdminView":
        //        MainView!.BtnHome.IsChecked = false;
        //        MainView!.BtnVerwaltung.IsChecked = false;
        //        MainView!.BtnAdmin.IsChecked = true;
        //        break;
        //}
    }

    public static void ShowLoginView(bool bAsStartLogin)
    {
        try
        {
            var pageService = ServiceProvider?.GetService<LoginView>();
            if (pageService == null) return;
            if (pageService is LoginView loginPage)
            {
                Show(loginPage, true);

                if (bAsStartLogin)
                {
                    loginPage.GridGoBack.Visibility = Visibility.Collapsed;
                    loginPage.BtnBeenden.Visibility = Visibility.Visible;
                }
                else
                {
                    loginPage.GridGoBack.Visibility = Visibility.Visible;
                    loginPage.BtnBeenden.Visibility = Visibility.Collapsed;
                }

                MainView!.ShowCloseButton = false;
                GoBackPage = null;
                MainView!.UserPanel.Visibility = Visibility.Hidden;
            }
        }
        catch (Exception ) 
        { 
        
        }
    }

    public static void GoBackOrToHome()
    {
        if(GoBackPage == null)
        {
            ShowPageOnMainView<HomeView>();
            return;
        }

        Show(GoBackPage);
    }
    
    private static void ActivateViwModel(UserControl iPage)
    {
        var pageVM = iPage.DataContext as BaseViewModel;
        if (pageVM != null) pageVM.IsActive = true;
    }

    public static void ShowUnderPageOn<T>(AnimatedContentControl animatedContentControl) where T : UserControl
    {
        try
        {
            var pageService = ServiceProvider?.GetService<T>();
            if (pageService == null) return;
            if (pageService is UserControl ucPage)
            {
                animatedContentControl.PagePlace.Content = null;
                animatedContentControl.PagePlace.Content = ucPage;

                animatedContentControl.MetroTabItem.IsSelected = false;
                animatedContentControl.MetroTabItem.IsSelected = true;
                
                ActivateViwModel(ucPage);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }

    private static bool ShowInformationWindow(string sMessage)
    {
        return new InfoWindow(sMessage, IWDialogType.Information).ShowDialog() ?? false;
    }

    private static bool ShowConfirmationWindow(string sMessage)
    {
        return new InfoWindow(sMessage, IWDialogType.Confirmation).ShowDialog() ?? false;
    }

    private static string ShowInputWindow(string sMessage)
    {
        var myInfoWindow = new InfoWindow(sMessage, IWDialogType.Input);
        myInfoWindow.ShowDialog();
        return myInfoWindow.InputText;
    }

    private static void ShowMainInfoFlyout(string sMessage, bool bWarnung)
    {
        MainView!.LblFlyoutInfo.Content = sMessage;
        MainView!.LblFlyoutInfo.Foreground = (bWarnung) ? Brushes.DarkRed : Brushes.White;
        MainView!.InfoFlyout.IsOpen = true;
    }

    public static void InitBaseDelEvents(BaseViewModel baseViewModel)
    {
        baseViewModel.DelShowLoginView += (IsStart) => ShowLoginView(IsStart);
        baseViewModel.DelGoBackOrGotoHome += () => GoBackOrToHome();

        baseViewModel.DelShowInformationWindow += (msg) => ShowInformationWindow(msg);
        baseViewModel.DelShowConfirmationWindow += (msg) => ShowConfirmationWindow(msg);
        baseViewModel.DelShowInputWindow += (msg) => ShowInputWindow(msg);

        baseViewModel.DelShowMainInfoFlyout += (msg, warnung) => ShowMainInfoFlyout(msg, warnung);
    }
}
