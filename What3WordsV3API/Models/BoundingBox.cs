using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curnow.Biz.What3WordsV3Net.Models
{
    public class BoundingBox
    {
        public double South { get; set; }
        public double West { get; set; }
        public double North { get; set; }
        public double East { get; set; }

        public override string ToString()
        {
            return $"{South},{West},{North},{East}";
        }
    }
}
