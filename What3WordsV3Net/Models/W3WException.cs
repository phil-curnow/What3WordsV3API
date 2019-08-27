using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curnow.Biz.What3WordsV3Net.Models
{
    public class W3WException : Exception
    {
        private string _code;
        private string _message;

        public W3WException(string code, string message)
        {
            this._code = code;
            this._message = message;
        }

        public string W3WCode
        {
            get { return _code; }
        }

        public string W3WMessage
        {
            get { return _message; }
        }
    }
}
