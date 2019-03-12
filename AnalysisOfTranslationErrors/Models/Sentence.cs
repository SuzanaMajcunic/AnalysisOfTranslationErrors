using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisOfTranslationErrors.Models
{
    public class Sentence : INotifyPropertyChanged
    {
        private int _Index = new int();
        public int Index
        {
            get { return _Index; }
            set
            {
                _Index = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Index)));
            }
        }

        private string _Text = "";
        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
