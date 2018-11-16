using Beskyddare.Utility;
using Beskyddare.XML.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Beskyddare.XML
{
    public static class XMLTools
    {
        private static List<XMLTypeConverter> typeConverters = new List<XMLTypeConverter>();

        public static IEnumerable<XMLTypeConverter> TypeConverters
        {
            get
            {
                foreach(XMLTypeConverter converter in typeConverters)
                {
                    yield return converter;
                }
            }
        }

        static XMLTools()
        {
            //On first use build up a list of possible type converters.
            RebuildTypeConverters();
        }

        public static void RebuildTypeConverters()
        {
            typeConverters.Clear();

            //Add
            List<XMLTypeConverter> temp = new List<XMLTypeConverter>();
            foreach (Type type in ReflectionUtility.GetInheritedTypes(typeof(XMLTypeConverter)))
            {
                //Log.Message($"Type converter found='{type.FullName}'");
                object typeConverter = Activator.CreateInstance(type);
                temp.Add((XMLTypeConverter)typeConverter);
            }

            //Order
            typeConverters.AddRange(temp.OrderBy(converter => converter.Order));

            Log.Message("Type converter order:");
            foreach (XMLTypeConverter converter in typeConverters)
            {
                Log.Message($"   {converter.GetType().FullName}");
            }
        }

        public static XMLTypeConverter GetXMLProperTypeConverter(Type type)
        {
            foreach (XMLTypeConverter converter in TypeConverters)
            {
                if (converter.CanConvertType(type))
                {
                    return converter;
                }
            }

            return null;
        }

        public static object ObjectFromField(object targetObject, FieldInfo field, XmlNode rootNode, out bool foundValidObject)
        {
            Type fieldType = field.FieldType;
            //Log.Message($"Field='{field.Name}' Type='{fieldType.Name}'");
            object result = null;
            foundValidObject = false;

            if(GetXMLProperTypeConverter(fieldType) is XMLTypeConverter converter)
            {
                result = converter.ConvertFromXML(fieldType, rootNode, field, targetObject);
                //Log.Message($"result={result}");
                foundValidObject = true;
            }
            
            return result;
        }

        public static void FillObjectFieldsFromXMLNode(object targetObject, Type objectType, XmlNode rootNode)
        {
            if (objectType.IsClass)
            {
                foreach (FieldInfo field in objectType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
                {
                    XmlNode targetNode = rootNode.SelectSingleNode(field.Name);
                    if(targetNode != null)
                    {
                        //Log.Message($"Field='{field.Name}' in Node='{rootNode.Name}' in Target Object='{targetObject.ToString()}'");
                        bool foundValidObject = false;
                        object fieldValue = ObjectFromField(targetObject, field, targetNode, out foundValidObject);
                        if(foundValidObject)
                        {
                            field.SetValue(targetObject, fieldValue);
                        }
                    }
                }
            }
        }

        public static object ObjectFromXMLNode(XmlNode rootNode, Type type)
        {
            object result = Activator.CreateInstance(type);

            FillObjectFieldsFromXMLNode(result, type, rootNode);

            return result;
        }

        public static T ObjectFromXMLNode<T>(XmlNode rootNode)
        {
            T result = Activator.CreateInstance<T>();
            Type objectType = result.GetType();

            FillObjectFieldsFromXMLNode(result, objectType, rootNode);

            return result;
        }
    }
}
