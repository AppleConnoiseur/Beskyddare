using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.XML
{
    public interface IExposeData
    {
        void ExposeData(XMLScribe scribe);
    }
}
