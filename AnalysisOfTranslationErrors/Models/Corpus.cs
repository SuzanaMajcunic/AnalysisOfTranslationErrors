using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisOfTranslationErrors.Models
{
    public class Corpus : INotifyPropertyChanged
    {
        private string _ProjectName = " New Project";
        public string ProjectName
        {
            get { return _ProjectName; }
            set
            {
                _ProjectName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjectName)));

            }
        }

        private ObservableCollection<Sentence> _SourceSentences = new ObservableCollection<Sentence>();
        public ObservableCollection<Sentence> SourceSentences
        {
            get { return _SourceSentences; }
            set
            {
                _SourceSentences = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SourceSentences)));
            }
        }


        private ObservableCollection<Sentence> _ReferenceSentences = new ObservableCollection<Sentence>();
        public ObservableCollection<Sentence> ReferenceSentences
        {
            get { return _ReferenceSentences; }
            set
            {
                _ReferenceSentences = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReferenceSentences)));
            }
        }


        private ObservableCollection<SysSentence> _SystemSentences = new ObservableCollection<SysSentence>();
        public ObservableCollection<SysSentence> SystemSentences
        {
            get { return _SystemSentences; }
            set
            {
                _SystemSentences = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SystemSentences)));
            }
        }

        private ObservableCollection<Sentence> _ModifiedSystemSentences = new ObservableCollection<Sentence>();
        public ObservableCollection<Sentence> ModifiedSystemSentences
        {
            get { return _ModifiedSystemSentences; }
            set
            {
                _ModifiedSystemSentences = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModifiedSystemSentences)));
            }
        }

        private ObservableCollection<long> _TimeOfEditSen = new ObservableCollection<long>();
        public ObservableCollection<long> TimeOfEditSen
        {
            get { return _TimeOfEditSen; }
            set
            {
                _TimeOfEditSen = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeOfEditSen)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
