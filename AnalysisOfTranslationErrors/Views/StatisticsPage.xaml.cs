using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;


namespace AnalysisOfTranslationErrors
{
    public sealed partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            this.InitializeComponent();
            ViewModel.LoadStatistics(FirstChart, SecondChart);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.Corpus == null) await new Windows.UI.Popups.MessageDialog("Please load a project.").ShowAsync();
            else
            {
                if (App.typologyApp == null) await new Windows.UI.Popups.MessageDialog("Please define MQM typology before calculating statistics.").ShowAsync();
                else
                {
                    await ViewModel.GenerateAnnoTreeAsync(resultTreeView);
                    (PieChart.Series[0] as PieSeries).ItemsSource = App.typologyApp.Dimensions;
                    //(ColumnChart.Series[0] as ColumnSeries).ItemsSource = ViewModel.GenerateDataColumnChart();
                    //(BubbleChart.Series[0] as BubbleSeries).ItemsSource = ViewModel.GenerateDataColumnChart();
                    (LineChart.Series[0] as LineSeries).ItemsSource = ViewModel.GenerateDataColumnChart();
                }
            }

        }

    }
}
