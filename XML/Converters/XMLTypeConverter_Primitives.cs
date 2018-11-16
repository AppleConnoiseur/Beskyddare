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
    public class XMLTypeConverter_Primitives : XMLTypeConverter
    {
        public override bool CanConvertType(Type type)
        {
            if(type == typeof(int) || type == typeof(float) || type == typeof(double) || type == typeof(bool) || type == typeof(string))
            {
                return true;
            }

            return false;
        }

        public override object ConvertFromXML(Type type, XmlNode rootNode, MemberInfo info = null, object targetObject = null)
        {
            //Log.Message($"Primitive={rootNode.InnerXml}");
            if (type == typeof(int))
            {
                return int.Parse(rootNode.InnerText);
            }
            else if (type == typeof(float))
            {
                return float.Parse(rootNode.InnerText);
            }
            else if (type == typeof(double))
            {
                return double.Parse(rootNode.InnerText);
            }
            else if (type == typeof(bool))
            {
                return bool.Parse(rootNode.InnerText);
            }
            else if (type == typeof(string))
            {
                return rootNode.InnerText;
            }

            return null;
        }

        public override void SaveToXML(object targetObject, XMLScribe scribe)
        {
            if(targetObject == null)
            {
                return;
            }

            Type type = targetObject.GetType();
            //Log.Message($"Value='{targetObject?.ToString()}'");

            if (type == typeof(int))
            {
                int value = (int)targetObject;
                scribe.Current.InnerText = value.ToString();
            }
            else if (type == typeof(float))
            {
                float value = (float)targetObject;
                scribe.Current.InnerText = value.ToString();
            }
            else if (type == typeof(double))
            {
                double value = (double)targetObject;
                scribe.Current.InnerText = value.ToString();
            }
            else if (type == typeof(bool))
            {
                bool value = (bool)targetObject;
                scribe.Current.InnerText = value.ToString();
            }
            else if (type == typeof(string))
            {
                string value = (string)targetObject;
                scribe.Current.InnerText = value;
            }
        }
    }
}
