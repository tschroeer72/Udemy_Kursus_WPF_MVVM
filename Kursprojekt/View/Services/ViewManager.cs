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
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }
}
