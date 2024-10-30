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

namespace Kursprojekt.View.Pages
{
    /// <summary>
    /// Interaktionslogik für VerwaltungView.xaml
    /// </summary>
    public partial class VerwaltungView : UserControl
    {
        public VerwaltungViewModel VerwaltungViewModel { get; }

        public VerwaltungView(VerwaltungViewModel verwaltungViewModel)
        {
            InitializeComponent();
            VerwaltungViewModel = verwaltungViewModel;
            DataContext = VerwaltungViewModel;
            VerwaltungViewModel.InitBaseViewModelDelegateAndEvents();
            OpenUnderPage(BtnBuchung, new RoutedEventArgs());
            BtnBuchung.IsChecked = true;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            ViewManager.GoBackOrToHome();
        }

        private void OpenUnderPage(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton objButton)
            {
                switch (objButton.Name)
                {
                    case "BtnBuchung":
                        ViewManager.ShowUnderPageOn<BuchungView>(AnimatedContentControl);
                        break;
                    case "BtnHaus":
                        ViewManager.ShowUnderPageOn<HausView>(AnimatedContentControl);
                        break;                    
                }
            }
        }
    }
}
