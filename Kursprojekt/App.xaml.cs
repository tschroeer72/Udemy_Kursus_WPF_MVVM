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
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        //MainView mainView = new();
        //mainView.Show();

        var mainView = _ServiceProvider.GetService<MainView>();
        if(mainView == null)
        {
            MessageBox.Show("Problem im _ServiceProvider");
            Current.Shutdown();
        }

        mainView!.Show();

        base.OnStartup(e);
    }
}
