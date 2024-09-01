using System.Configuration;
using System.Data;
using System.Windows;

namespace Kursprojekt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow mainView = new();
            mainView.Show();

            base.OnStartup(e);
        }
    }

}
