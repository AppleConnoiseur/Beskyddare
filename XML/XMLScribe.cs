using Beskyddare.Utility;
using Beskyddare.XML.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Beskyddare.XML
{
    /// <summary>
    /// Helps navigating through XML documents.
    /// </summary>
    public class XMLScribe
    {
        private XmlDocument document;
        private XmlNode currentNode;
        private string filePath;
        public ScribeMode mode;

        public string DocumentPath
        {
            get
            {
                return filePath;
            }
        }

        public XmlDocument Document
        {
            get
            {
                return document;
            }
        }

        public XmlNode Current
        {
            get
            {
                return currentNode;
            }
        }

        public XMLScribe(XmlDocument document)
        {
            this.document = document;
            mode = ScribeMode.Saving;
        }

        public XMLScribe(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            if (System.IO.File.Exists(filePath))
            {
                doc.Load(filePath);
                mode = ScribeMode.Loading;
            }
            document = doc;
            currentNode = document;
            this.filePath = filePath;
        }

        public void SaveDocument(string savePath = null)
        {
            if (savePath != null)
            {
                filePath = savePath;
            }

            document.Save(filePath);
        }

        public XmlNode EnterNode(string name)
        {
            if(Current == null)
            {
                return null;
            }
            if(Current.SelectSingleNode(name) is XmlNode enterNode)
            {
                currentNode = enterNode;
                return enterNode;
            }
            else
            {
                if(mode == ScribeMode.Saving)
                {
                    XmlNode newEnterNode = document.CreateElement(name);
                    currentNode = Current.AppendChild(newEnterNode);
                    return newEnterNode;
                }
                return null;
            }
        }

        public XmlNode EnterNode(XmlNode enterNode)
        {
            if (enterNode == null)
            {
                return null;
            }
            if(enterNode.OwnerDocument != document)
            {
                Log.Error($"Tried to enter a node in another document! Node='{(enterNode.Name)}' in Document='{DocumentPath}'");
                return null;
            }

            currentNode = enterNode;
            return enterNode;
        }

        public void LookupValue<T>(ref T value, string key, T defaultValue = default(T), bool saveNull = false)
        {
            switch(mode)
            {
                case ScribeMode.Saving:
                    {
                        XmlNode valueNode = EnterNode(key);
                        SaveValueIntoNode(value, saveNull);
                        ExitNode();
                    }
                    break;

                case ScribeMode.Loading:
                    {
                        if(Current.SelectSingleNode(key) is XmlNode valueNode)
                        {
                            EnterNode(valueNode);

                            Type type = typeof(T);
                            XMLTypeConverter converter = XMLTools.GetXMLProperTypeConverter(type);
                            if (converter == null)
                            {
                                Log.Error($"Found no suitable converter for Node='{(Current.Name)}' in Document='{DocumentPath}'");
                            }
                            else
                            {
                                Log.Message($"Converting with '{converter.GetType().Name}' Node='{valueNode.Name}' with Type='{type.Name}' with Value='{valueNode.InnerXml}'");
                                object objectValue = converter.ConvertFromXML(type, Current);
                                if(objectValue != null && objectValue.GetType() != value.GetType())
                                {
                                    Log.Error($"Type mismatch when converting. Value='{objectValue}' in Node='{valueNode.Name}'");
                                }
                                else
                                {
                                    value = (T)objectValue;
                                }
                            }

                            ExitNode();
                        }
                    }
                    break;
            }
        }

        public void SaveValueIntoNode(object value, bool saveNull = false)
        {
            if(value == null && !saveNull)
            {
                return;
            }

            Type type = value.GetType();
            //Log.Message($"Saving value='{value?.ToString()}'");
            XMLTypeConverter converter = XMLTools.GetXMLProperTypeConverter(type);
            if (converter == null)
            {
                Log.Error($"Found no suitable converter for Node='{(Current.Name)}' in Document='{DocumentPath}'");
            }
            else
            {
                converter.SaveToXML(value, this);
            }
        }

        public void ExitNode()
        {
            if (Current != null)
            {
                currentNode = Current.ParentNode;
            }
        }

        public enum ScribeMode
        {
            None,
            Saving,
            Loading
        }
    }
}
