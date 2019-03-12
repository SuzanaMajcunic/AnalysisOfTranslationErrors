using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisOfTranslationErrors.Models
{
    public class SysSentence : INotifyPropertyChanged
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

        private ObservableCollection<SysAnnotation> _Annotations = new ObservableCollection<SysAnnotation>();
        public ObservableCollection<SysAnnotation> Annotations
        {
            get { return _Annotations; }
            set
            {
                _Annotations = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Annotations)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
