using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curnow.Biz.What3WordsV3Net.Models
{
    public class Error
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class W3WError
    {
        public Error error { get; set; }
    }
}
