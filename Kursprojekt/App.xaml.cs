using Kursprojekt.View.Pages;
using Kursprojekt.View.Windows;
using Kursprojekt.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Kursprojekt;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider _ServiceProvider;
    
    public App()
    {
        ServiceCollection ServiceCollection = new();
        ConfigureServices(ServiceCollection);
        _ServiceProvider = ServiceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<MainView>();
        services.AddSingleton<MainViewModel>();

        services.AddSingleton<HomeView>();
        services.AddSingleton<HomeViewModel>();

        services.AddSingleton<AdminView>();
        services.AddSingleton<AdminViewModel>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        //MainView mainView = new();
        //mainView.Show();

        var mainView = _ServiceProvider.GetService<MainView>();
        if(mainView == null)
        {
            MessageBox.Show("Problem im ServiceProvider");
            Current.Shutdown();
        }

        mainView!.ServiceProvider = _ServiceProvider;
        mainView!.Show();

        base.OnStartup(e);
    }
}
