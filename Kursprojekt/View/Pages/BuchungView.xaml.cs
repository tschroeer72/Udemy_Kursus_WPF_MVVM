using Kursprojekt.DTOs;
using Kursprojekt.View.Services;
using Kursprojekt.ViewModel;
using LiveCharts.Wpf;
using LiveCharts;
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
/// Interaktionslogik für BuchungView.xaml
/// </summary>
public partial class BuchungView : UserControl
{
    public BuchungViewModel BuchungViewModel { get; }

    public BuchungView(BuchungViewModel buchungViewModel)
    {
        InitializeComponent();
        BuchungViewModel = buchungViewModel;
        DataContext = BuchungViewModel;
        BuchungViewModel.InitBaseViewModelDelegateAndEvents();

        BuchungViewModel.DelSchowBarChartData += (lst) => ShowBarChartData(lst);
    }

    private void ShowBarChartData(List<BarChartDto> iBarChartDtoLst)
    {
        var BarSerieCollection = new SeriesCollection();
        string[] Labels = new string[iBarChartDtoLst.Count];
        ChartValues<double> CharData = new();

        Brush color;
        try
        {
            color = (Brush)Application.Current.Resources["AppColorCyan"];
        }
        catch
        {
            color = Brushes.Teal;
        }

        for (int i = 0; i < iBarChartDtoLst.Count; i++)
        {
            Labels[i] = iBarChartDtoLst[i].AuftragID;
            CharData.Add(iBarChartDtoLst[i].HausPreis);
        }


        BarSerieCollection.Add(new ColumnSeries
        {
            Title = $"PREIS",
            Values = CharData,
            DataLabels = true,
            FontSize = 32,
            MaxColumnWidth = 100,
            LabelsPosition = BarLabelPosition.Top,
            Fill = color
        });


        BarAxisX.Labels = Labels;
        BarChart.Series = BarSerieCollection;
    }
}
