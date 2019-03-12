using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisOfTranslationErrors.Models
{
    public class Typology
    {
        public ObservableCollection<Dimension> Dimensions { get; set; }

        public Typology()
        {
            Dimensions = new ObservableCollection<Dimension>();
        }
    }
}
