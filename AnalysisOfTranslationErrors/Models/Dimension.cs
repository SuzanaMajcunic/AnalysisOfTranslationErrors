using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisOfTranslationErrors.Models
{
    public class Dimension
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }

        public string Color { get; set; }

        public int Errors { get; set; }
    }
}
