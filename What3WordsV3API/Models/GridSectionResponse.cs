using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curnow.Biz.What3WordsV3Net.Models
{
    public class Start
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class End
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class Line
    {
        public Start start { get; set; }
        public End end { get; set; }
    }

    public class GridSectionResponse
    {
        public List<Line> lines { get; set; }
    }
}
