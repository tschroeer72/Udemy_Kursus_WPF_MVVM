using Kursprojekt.View.Services;
using Kursprojekt.ViewModel;
using Microsoft.Win32;
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
        HausViewModel.InitBaseViewModelDelegateAndEvents();

        HausViewModel.DelShowFileDialog += () => ShowFileDialog();
    }

    private string ShowFileDialog()
    {
        try
        {
            OpenFileDialog dlgOpenFile = new()
            {
                Filter = "Image Files jpg, png, gif| *.jpg;*.png;*.gif",
                Multiselect = false
            };

            var result = dlgOpenFile.ShowDialog();
            if (result == true && result.HasValue)
            {
                return dlgOpenFile.FileName;
            }

            return "";
        }
        catch (Exception ex)
        {
            ViewManager.ShowInformationWindow(ex.Message);
            return "";
        }
    }
}
