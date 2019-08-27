using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curnow.Biz.What3WordsV3Net.Models
{
    public class Language
    {
        public string nativeName { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class LanguagesResponse
    {
        public List<Language> languages { get; set; }
    }
}
