using Beskyddare.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Beskyddare.DataBinding
{
    /// <summary>
    /// Stores all the data bindings in a database.
    /// </summary>
    public static class DataBindingDatabase
    {
        private static List<NodeDataBinding> bindings = new List<NodeDataBinding>();
        //private static Dictionary<string, DataBinding> bindingsDictionary = new Dictionary<string, DataBinding>();

        public static string BindingDirectory
        {
            get
            {
                return FileSystem.DataDirectory + "/Bindings";
            }
        }

        public static List<NodeDataBinding> Bindings
        {
            get
            {
                return bindings;
            }
        }

        public static void ReloadDataBindings()
        {
            bindings.Clear();

            IEnumerable<string> files = Directory.EnumerateFiles(BindingDirectory);
            if (files != null)
            {
                foreach (string filePath in files.Where(path => path.ToLower().EndsWith(".xml")))
                {
                    string path = filePath.FixPath();
                    Log.Message("Bindings file detected: " + path);

                    XmlDocument document = new XmlDocument();
                    try
                    {
                        document.Load(path);
                        LoadBindingsFromXML(document, path);
                    }
                    catch (Exception exception)
                    {
                        Log.Error($"Error loading '{path}' with Exception: {exception.Message}");
                    }
                }
            }
        }

        public static void LoadBindingsFromXML(XmlDocument document, string path)
        {
            if (!document.HasChildNodes)
            {
                throw new Exception($"'{path}' contain no child nodes!");
            }

            //First node.
            if (document.ChildNodes.Count > 0)
            {
                XmlNode rootNode = document.DocumentElement.SelectSingleNode("/DataBindings");
                if (rootNode == null)
                {
                    throw new Exception($"'{path}' contain no 'DataBindings' node!");
                }

                foreach(XmlNode bindingNode in rootNode.ChildNodes)
                {
                    if(bindingNode.Name.ToLower() == "binding")
                    {
                        //Create a new binding.
                        NodeDataBinding binding = new NodeDataBinding();
                        bool gotAnyBinding = false;

                        if (bindingNode.SelectSingleNode("Parent") is XmlNode parentNode)
                        {
                            binding.parentNode = parentNode.InnerText;
                        }

                        if (bindingNode.SelectSingleNode("TargetNode") is XmlNode targetNode)
                        {
                            binding.targetNode = targetNode.InnerText;
                            //Log.Message($"Target node set to '{binding.targetNode}'");
                        }

                        if (bindingNode.SelectSingleNode("Values") is XmlNode valuesNode)
                        {
                            foreach (XmlNode valueNode in valuesNode.ChildNodes)
                            {
                                gotAnyBinding = true;
                                string key = valueNode.Name;
                                object value = null;

                                if (valueNode.Attributes.GetNamedItem("Type") is XmlNode typeAttribute)
                                {
                                    string typeValueName = typeAttribute.InnerText.ToLower();

                                    if(typeValueName == "string")
                                    {
                                        value = valueNode.InnerText;
                                        binding.values.Add(key, value);
                                    }
                                    else if (typeValueName == "int")
                                    {
                                        if(int.TryParse(valueNode.InnerText, out int result))
                                        {
                                            value = result;
                                            binding.values.Add(key, value);
                                        }
                                    }
                                    else if (typeValueName == "float")
                                    {
                                        if (float.TryParse(valueNode.InnerText, out float result))
                                        {
                                            value = result;
                                            binding.values.Add(key, value);
                                        }
                                    }
                                    else if (typeValueName == "double")
                                    {
                                        if (double.TryParse(valueNode.InnerText, out double result))
                                        {
                                            value = result;
                                            binding.values.Add(key, value);
                                        }
                                    }
                                    else if (typeValueName == "boolean")
                                    {
                                        if (bool.TryParse(valueNode.InnerText, out bool result))
                                        {
                                            value = result;
                                            binding.values.Add(key, value);
                                        }
                                    }
                                }
                                else
                                {
                                    //Default to string.
                                    value = valueNode.InnerText;
                                    binding.values.Add(key, value);
                                }

                                //Log.Message($"key='{key}', value type='{value.GetType().FullName}', value='{value}'");
                            }
                        }

                        if (gotAnyBinding)
                        {
                            bindings.Add(binding);
                        }
                    }
                    else
                    {
                        Log.Error($"Wrong name for node='{bindingNode.Name}', must be 'Binding'!");
                    }
                }
            }
        }
    }
}
