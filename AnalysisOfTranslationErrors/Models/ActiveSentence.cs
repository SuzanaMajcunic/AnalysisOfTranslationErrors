using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisOfTranslationErrors.Models
{
    public class ActiveSentence : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _ActiveIndex = new int();
        public int ActiveIndex
        {
            get { return _ActiveIndex; }
            set
            {
                _ActiveIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveIndex)));
            }
        }

        private Sentence _ActiveSrcSentence;
        public Sentence ActiveSrcSentence
        {
            get { return _ActiveSrcSentence; }
            set
            {
                _ActiveSrcSentence = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveSrcSentence)));
            }
        }

        private SysSentence _ActiveSysSentence;
        public SysSentence ActiveSysSentence
        {
            get { return _ActiveSysSentence; }
            set
            {
                _ActiveSysSentence = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveSysSentence)));
            }
        }



        private Sentence _ActiveRefSentence;
        public Sentence ActiveRefSentence
        {
            get { return _ActiveRefSentence; }
            set
            {
                _ActiveRefSentence = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveRefSentence)));
            }
        }

        private Sentence _ActiveModSysSentence;
        public Sentence ActiveModSysSentence
        {
            get { return _ActiveModSysSentence; }
            set
            {
                _ActiveModSysSentence = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveModSysSentence)));
            }
        }
    }
}
