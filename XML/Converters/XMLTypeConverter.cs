using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Beskyddare.XML.Converters
{
    public class XMLTypeConverter
    {
        public virtual int Order
        {
            get
            {
                return 0;
            }
        }

        public virtual bool CanConvertType(Type type)
        {
            return false;
        }

        public virtual object ConvertFromXML(Type type, XmlNode rootNode, MemberInfo info = null, object targetObject = null)
        {
            return null;
        }

        public virtual void SaveToXML(object targetObject, XMLScribe scribe)
        {
            
        }
    }
}
