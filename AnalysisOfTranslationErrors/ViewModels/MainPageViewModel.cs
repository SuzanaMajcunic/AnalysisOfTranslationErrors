using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisOfTranslationErrors.ViewModels
{
    public class MainPageViewModel
    {
        Services.FileService fileService = new Services.FileService();


        public async void SaveProject()
        {
            await fileService.SaveProjectAndAskAsync();

        }

        public async void SaveStatistics()
        {
            await fileService.SaveStatisticsFileAsync();

        }

        public async void SaveChartOne()
        {
            if (App.StatisticsData != null)
            {
                await fileService.SaveChartAsPNGAsync(App.StatisticsData.ChartOne, "ChartOne");
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog("Can't save the chart - no statistical data.").ShowAsync();
            }
        }

        public async void SaveChartTwo()
        {
            if (App.StatisticsData != null)
            {
                await fileService.SaveChartAsPNGAsync(App.StatisticsData.ChartTwo, "ChartTwo");
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog("Can't  save the chart - no statistical data.").ShowAsync();
            }
        }
    }
}
