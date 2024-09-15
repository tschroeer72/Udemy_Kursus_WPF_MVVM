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
/// Interaktionslogik für HausView.xaml
/// </summary>
public partial class HausView : UserControl
{
    public HausViewModel HausViewModel { get; }

    public HausView(HausViewModel hausViewModel)
    {
        InitializeComponent();
        HausViewModel = hausViewModel;
        DataContext = HausViewModel;

        ViewManager.InitBaseDelEvents(HausViewModel);
    }
    
}
