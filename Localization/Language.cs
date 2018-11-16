using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Localization
{
    public class Language
    {
        public string name = "No name";
        public bool defaultLanguage = false;
        public Dictionary<string, string> keyed = new Dictionary<string, string>();
    }
}
