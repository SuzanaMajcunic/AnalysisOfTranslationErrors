using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisOfTranslationErrors.Models
{
    public class RecentProject : INotifyPropertyChanged
    {
        private string _FileName = "No recent projects...";
        public string FileName
        {
            get { return _FileName; }
            set
            {
                _FileName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileName)));

            }
        }

        private string _FileToken = "";
        public string FileToken
        {
            get { return _FileToken; }
            set
            {
                _FileToken = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileToken)));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
