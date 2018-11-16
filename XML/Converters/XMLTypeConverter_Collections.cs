using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.XML.Converters
{
    public class XMLTypeConverter_Collections : XMLTypeConverter
    {
        public override int Order => int.MaxValue / 2;

        public override bool CanConvertType(Type type)
        {
            if(type == typeof(List<>) || type == typeof(Dictionary<,>) || type == typeof(HashSet<>))
            {
                return true;
            }

            return false;
        }
    }
}
