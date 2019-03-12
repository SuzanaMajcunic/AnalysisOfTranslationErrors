using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AnalysisOfTranslationErrors.Models
{
    public class StatisticsData : INotifyPropertyChanged
    {
        private string _ProjectName = " No name";
        public string ProjectName
        {
            get { return _ProjectName; }
            set
            {
                _ProjectName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjectName)));

            }
        }

        private string _NumSentences = " None";
        public string NumSentences
        {
            get { return _NumSentences; }
            set
            {
                _NumSentences = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumSentences)));

            }
        }

        private string _NumErrorAll = " None";
        public string NumErrorAll
        {
            get { return _NumErrorAll; }
            set
            {
                _NumErrorAll = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumErrorAll)));

            }
        }
        private string _AvgErrorSen = " None";
        public string AvgErrorSen
        {
            get { return _AvgErrorSen; }
            set
            {
                _AvgErrorSen = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvgErrorSen)));

            }
        }

        private string _AvgTimeEdit = " HH:MM:SS";
        public string AvgTimeEdit
        {
            get { return _AvgTimeEdit; }
            set
            {
                _AvgTimeEdit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvgTimeEdit)));

            }
        }

        private StackPanel _ChartOne = new StackPanel();
        public StackPanel ChartOne
        {
            get { return _ChartOne; }
            set
            {
                _ChartOne = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChartOne)));

            }
        }

        private StackPanel _ChartTwo = new StackPanel();
        public StackPanel ChartTwo
        {
            get { return _ChartTwo; }
            set
            {
                _ChartTwo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChartTwo)));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
