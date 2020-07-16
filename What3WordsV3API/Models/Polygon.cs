using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curnow.Biz.What3WordsV3Net.Models
{
    public class Polygon
    {
        private List<Coordinates> coordinateList = new List<Coordinates>();

        public List<Coordinates> Coordinates { get; }

        public void Add(Coordinates coordinates)
        {
            if (coordinateList.Count == 25)
                throw new W3WException("ParameterError", "You can only add a maximum of 25 coordinate items to a Polygon object.");

            coordinateList.Add(coordinates);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            coordinateList.ForEach(c =>
            {
                sb.Append($"{c.lat},{c.lng},");
            });

            string list = sb.ToString();
            return list.Substring(0, list.Length - 1);
        }
    }
}
