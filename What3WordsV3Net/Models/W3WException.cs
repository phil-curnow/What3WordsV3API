using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Curnow.Biz.What3WordsV3Net.Models
{
    [Serializable]
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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentException("W3WException.GetObject: SerializationInfo cannot be null");

            info.AddValue("W3WCode", _code);
            info.AddValue("W3WMessage", _message);
            base.GetObjectData(info, context);
        }
    }
}
