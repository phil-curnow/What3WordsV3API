using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curnow.Biz.What3WordsV3Net.Models
{
    public class Circle
    {
        public Coordinates Coordinates { get; set; }
        public double Kilometres { get; set; }

        public override string ToString()
        {
            return $"{Coordinates.lat},{Coordinates.lng},{Kilometres}";
        }
    }
}
