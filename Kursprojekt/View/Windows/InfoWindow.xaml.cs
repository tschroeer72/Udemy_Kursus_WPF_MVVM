using Kursprojekt.Enums;
using Kursprojekt.View.Services;
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
using System.Windows.Shapes;

namespace Kursprojekt.View.Windows;

/// <summary>
/// Interaktionslogik für InfoWindow.xaml
/// </summary>
public partial class InfoWindow : Window
{
    private IWDialogType DialogType { get; set; } = new();
    public string InputText { get; set; } = "";

    public InfoWindow(string iMessage, IWDialogType iDialogType)
    {
        InitializeComponent();
        Owner = ViewManager.MainView;
        txtMessage.Text = iMessage;
        DialogType = iDialogType;

        BorderInputText.Visibility = Visibility.Collapsed;
        Grid_JA_NEIN.Visibility = Visibility.Collapsed;
        Grid_Btn_OK_BtnAbbrechen.Visibility = Visibility.Collapsed;

        switch (DialogType)
        {
            case IWDialogType.Information:
                lblTitle.Content = "Information";
                Grid_Btn_OK_BtnAbbrechen.Visibility = Visibility.Visible;
                BtnAbbrechen.Visibility = Visibility.Collapsed;
                break;
            case IWDialogType.Confirmation:
                lblTitle.Content = "Bestätigen";
                Grid_JA_NEIN.Visibility = Visibility.Visible;
                break;
            case IWDialogType.Input:
                lblTitle.Content = "Angabe";
                BorderInputText.Visibility = Visibility.Visible;
                Grid_Btn_OK_BtnAbbrechen.Visibility = Visibility.Visible;
                break;
            default:
                break;
        }

    }

    private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        try
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        catch (Exception)
        {
        }
    }
    private void BtnSchliessen_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
    private void BtnNein_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void BtnJa_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    private void BtnAbbrechen_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void BtnOK_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        if (DialogType == IWDialogType.Input)
        {
            InputText = txtInputText.Text;
        }

    }
}
