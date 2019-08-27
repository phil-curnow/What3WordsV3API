using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curnow.Biz.What3WordsV3Net.Models
{
    public class Suggestion
    {
        public string country { get; set; }
        public string nearestPlace { get; set; }
        public string words { get; set; }
        public int distanceToFocusKm { get; set; }
        public int rank { get; set; }
        public string language { get; set; }
    }

    public class AutoSuggestResponse
    {
        public List<Suggestion> suggestions { get; set; }
    }
}
