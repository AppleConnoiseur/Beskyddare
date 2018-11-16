using Beskyddare.Logic;
using Beskyddare.UI;
using Beskyddare.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Beskyddare.Localization
{
    public static class LanguageDatabase
    {
        private static Language activeLanguage;
        private static Language fallbackLanguage;

        private static List<Language> languages = new List<Language>();

        public static Language Active
        {
            get
            {
                return activeLanguage;
            }
        }

        public static List<Language> Languages
        {
            get
            {
                return languages;
            }
        }

        public static string LanguageDirectory
        {
            get
            {
                return FileSystem.DataDirectory + "/Language";
            }
        }

        public static string Translate(this string text)
        {
            if(activeLanguage != null)
            {
                if(activeLanguage.keyed.ContainsKey(text))
                {
                    return activeLanguage.keyed[text];
                }
            }

            if(fallbackLanguage != null)
            {
                if (fallbackLanguage.keyed.ContainsKey(text))
                {
                    return fallbackLanguage.keyed[text];
                }
            }

            return text;
        }

        public static void SetActiveLanguage(Language language)
        {
            activeLanguage = language;

            if(Game.Instance != null)
            {
                foreach (Translator translator in Game.Instance.translatorNodes)
                {
                    translator.DoTranslation();
                }
            }
        }

        public static void ReloadLanguages()
        {
            //Reset
            activeLanguage = null;
            languages.Clear();

            //Load languages
            IEnumerable<string> files = Directory.EnumerateFiles(LanguageDirectory);
            if (files != null)
            {
                foreach(string filePath in files.Where(path => path.ToLower().EndsWith(".xml")))
                {
                    string path = filePath.FixPath();
                    //Log.Message("Language file detected: " + path);

                    XmlDocument document = new XmlDocument();
                    try
                    {
                        document.Load(path);
                        Language language = LoadLanguageFromXML(document, path);
                        if(language != null)
                        {
                            languages.Add(language);
                        }
                    }
                    catch(Exception exception)
                    {
                        Log.Error($"Error loading '{path}' with Exception: {exception.Message}");
                    }
                }
            }
        }

        public static Language LoadLanguageFromXML(XmlDocument document, string path = null)
        {
            Language language = null;

            if (!document.HasChildNodes)
            {
                throw new Exception($"'{path}' contain no child nodes!");
            }

            //First node.
            if(document.ChildNodes.Count > 0)
            {
                XmlNode rootNode = document.DocumentElement.SelectSingleNode("/Language");
                if(rootNode == null)
                {
                    throw new Exception($"'{path}' contain no 'Language' node!");
                }

                language = new Language();

                XmlNode nameNode = rootNode.SelectSingleNode("Name");
                if(nameNode == null)
                {
                    language.name = "No name";
                    Log.Warning($"'{path}' contain no name.");
                }
                else
                {
                    language.name = nameNode.InnerText;
                    //Log.Message("Language name: " + language.name);
                }

                XmlNode defaultNode = rootNode.SelectSingleNode("Default");
                if(defaultNode != null && defaultNode?.InnerText.ToLower() == "true")
                {
                    language.defaultLanguage = true;

                    //Set this as our current language, for now.
                    if(activeLanguage == null)
                    {
                        activeLanguage = language;
                    }

                    //Sets the fallback language to use in case translation can't be found.
                    if (fallbackLanguage == null)
                    {
                        fallbackLanguage = language;
                        Log.Message($"Assigned fallback language to '{language.name}'.");
                    }
                }

                XmlNode keyedNode = rootNode.SelectSingleNode("Keyed");
                if(keyedNode != null)
                {
                    //Fill the dictionary.
                    foreach(XmlNode node in keyedNode)
                    {
                        if(!(node is XmlComment))
                        {
                            language.keyed[node.Name] = node.InnerText;
                            //Log.Message($"'{node.Name}'='{node.InnerText}'");
                        }
                    }
                }
            }
            
            return language;
        }
    }
}
