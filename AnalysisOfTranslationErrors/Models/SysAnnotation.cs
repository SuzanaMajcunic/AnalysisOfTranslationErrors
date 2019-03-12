using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisOfTranslationErrors.Models
{
    public class SysAnnotation : INotifyPropertyChanged
    {
        private int _StartPosition = new int();
        public int StartPosition
        {
            get { return _StartPosition; }
            set
            {
                _StartPosition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StartPosition)));
            }
        }

        private int _EndPosition = new int();
        public int EndPosition
        {
            get { return _EndPosition; }
            set
            {
                _EndPosition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EndPosition)));
            }
        }

        private Dimension _AnnoDim = new Dimension();
        public Dimension AnnoDim
        {
            get { return _AnnoDim; }
            set
            {
                _AnnoDim = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnnoDim)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
