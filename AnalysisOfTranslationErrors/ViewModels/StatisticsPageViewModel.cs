using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisOfTranslationErrors.Models;
using Windows.UI.Xaml.Controls;

namespace AnalysisOfTranslationErrors.ViewModels
{
    public class StatisticsPageViewModel
    {
        Services.FileService fileService = new Services.FileService();

        public void LoadStatistics(StackPanel firstChart, StackPanel secondChart)
        {
            if (App.Corpus != null)
            {
                App.StatisticsData = new StatisticsData();
                App.StatisticsData.ProjectName = " " + App.Corpus.ProjectName;
                App.StatisticsData.NumSentences = " " + App.Corpus.SourceSentences.Count();
                App.StatisticsData.NumErrorAll = " " + CountAllErrors();

                App.StatisticsData.AvgErrorSen = " " + AverageErrorPerSentence().ToString("R");
                //App.StatisticsData.AvgTimeEdit = " " + AverageTimeEdit();


                TimeSpan t = TimeSpan.FromMilliseconds(fileService.AverageTimeEdit());
                if (t.Hours == 0)
                {
                    App.StatisticsData.AvgTimeEdit = string.Format(" {0:D2}m:{1:D2}s:{2:D2}ms",
                                        t.Minutes,
                                        t.Seconds,
                                        t.Milliseconds);
                }
                else
                {
                    App.StatisticsData.AvgTimeEdit = string.Format(" {0:D2}h:{1:D2}m:{2:D2}s:{3:D2}ms",
                                        t.Hours,
                                        t.Minutes,
                                        t.Seconds,
                                        t.Milliseconds);
                }


                App.StatisticsData.ChartOne = firstChart;
                App.StatisticsData.ChartTwo = secondChart;

                GetErrorsPerNode();
            }

        }

        public int CountAllErrors()
        {
            int num = 0;
            foreach (var sent in App.Corpus.SystemSentences)
            {
                num += sent.Annotations.Count();
            }

            return num;
        }

        public Single AverageErrorPerSentence()
        {
            Single allErrors = CountAllErrors();
            Single numSent = App.Corpus.SourceSentences.Count();
            return allErrors / numSent;
        }


        public void GetErrorsPerNode()
        {
            // ToDo
            int counter = 0;
            if (App.typologyApp != null)
            {
                for (int i = 0; i < App.typologyApp.Dimensions.Count(); i++)
                {
                    counter = 0;
                    foreach (var sysSen in App.Corpus.SystemSentences)
                    {
                        var rez = sysSen.Annotations.Where(a => a.AnnoDim == App.typologyApp.Dimensions.ElementAt(i)).ToList();
                        counter += rez.Count();
                    }
                    App.typologyApp.Dimensions.ElementAt(i).Errors = counter;
                }
            }


        }

        public List<StatisticData> GenerateDataColumnChart()
        {
            int numAnno = 0;
            Dictionary<int, int> statisticData = new Dictionary<int, int>();

            foreach (var sysSen in App.Corpus.SystemSentences)
            {
                numAnno = sysSen.Annotations.Count();

                if (statisticData.ContainsKey(numAnno)) statisticData[numAnno] = statisticData[numAnno] + 1;
                else statisticData.Add(numAnno, 1);
            }

            List<StatisticData> statisticList = new List<StatisticData>();
            foreach (KeyValuePair<int, int> pair in statisticData.OrderBy(key => key.Key))
            {
                statisticList.Add(new StatisticData() { NumAnno = pair.Key, NumSent = pair.Value });
            }

            return statisticList;
        }



        public async Task GenerateAnnoTreeAsync(TreeView annoTreeView)
        {

            annoTreeView.RootNodes.Clear();

            if (App.typologyApp != null)
            {
                foreach (var dim in App.typologyApp.Dimensions)
                {
                    if (dim.ParentId == "ROOT")
                    {
                        List<Models.Dimension> children = App.typologyApp.Dimensions.Where(p => p.ParentId == dim.Id).ToList();
                        if (children.Any())
                        {
                            TreeViewNode nodeWithChildren = new TreeViewNode() { Content = dim };
                            nodeWithChildren.IsExpanded = true;
                            foreach (var child in children)
                            {
                                DimensionToNodeRecursion(nodeWithChildren, child, App.typologyApp);
                            }
                            annoTreeView.RootNodes.Add(nodeWithChildren);
                        }
                        else
                        {
                            annoTreeView.RootNodes.Add(new TreeViewNode() { Content = dim });
                        }
                    }
                }
            }
            else await new Windows.UI.Popups.MessageDialog("Can not initialize annotation tree view. Model is not loaded!").ShowAsync();
        }

        public void DimensionToNodeRecursion(TreeViewNode root, Dimension childDim, Typology model)
        {
            TreeViewNode tmpNode = new TreeViewNode() { Content = childDim };
            tmpNode.IsExpanded = true;
            List<Models.Dimension> children = model.Dimensions.Where(p => p.ParentId == childDim.Id).ToList();

            if (children.Any())
            {
                foreach (var c in children)
                {
                    DimensionToNodeRecursion(tmpNode, c, model);
                }
                root.Children.Add(tmpNode);
            }
            else
            {
                root.Children.Add(tmpNode);
            }
        }





    }

    public class StatisticData
    {
        public int NumAnno { get; set; }
        public int NumSent { get; set; }
    }
}
