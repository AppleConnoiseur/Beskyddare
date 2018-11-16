using Beskyddare.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Beskyddare.XML.Converters
{
    public class XMLTypeConverter_Object : XMLTypeConverter
    {
        public override int Order => int.MaxValue;

        public override bool CanConvertType(Type type)
        {
            return true;
        }

        public override object ConvertFromXML(Type type, XmlNode rootNode, MemberInfo info = null, object targetObject = null)
        {
            return XMLTools.ObjectFromXMLNode(rootNode, type);
        }

        public override void SaveToXML(object targetObject, XMLScribe scribe)
        {
            //Log.Message($"[Object] SaveToXML='{targetObject?.ToString()}'");
            if (targetObject == null)
            {
                return;
            }

            Type type = targetObject.GetType();

            //Custom behavior.
            if(targetObject is IExposeData exposed)
            {
                exposed.ExposeData(scribe);
                return;
            }

            //Default behavior.
            if (type.IsClass)
            {
                foreach (FieldInfo field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                {
                    //Log.Message($"Field='{field.Name}'");
                    XMLTypeConverter converter = XMLTools.GetXMLProperTypeConverter(field.FieldType);
                    if(converter == null)
                    {
                        Log.Error($"Found no suitable converter for Node='{(scribe.Current.Name + '/' + field.Name)}' in Document='{scribe.DocumentPath}'");
                    }
                    else
                    {
                        object targetObjectField = field.GetValue(targetObject);
                        XmlNode targetNode = scribe.EnterNode(field.Name);
                        converter.SaveToXML(targetObjectField, scribe);
                        scribe.ExitNode();
                    }
                }
            }
        }
    }
}
