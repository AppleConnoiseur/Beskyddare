using Beskyddare.Utility;
using Beskyddare.XML;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Beskyddare.Logic
{
    /// <summary>
    /// Stores settings for the game.
    /// </summary>
    public static class Settings
    {
        private static SettingsData data = new SettingsData();

        public static SettingsData Data
        {
            get
            {
                return data;
            }
        }

        public static string SettingsFileName
        {
            get
            {
                return "Settings.xml";
            }
        }

        public static string SettingsPath
        {
            get
            {
                return OS.GetUserDataDir() + '/' + SettingsFileName;
            }
        }

        public static void ReloadSettings()
        {
            Log.Message($"Settings file: {SettingsPath}");

            if(System.IO.File.Exists(SettingsPath))
            {
                Log.Message("Settings file found.");
                XMLScribe scribe = new XMLScribe(SettingsPath);
                scribe.mode = XMLScribe.ScribeMode.Loading;
                scribe.EnterNode("Settings");
                {
                    scribe.LookupValue(ref data, "SettingsData");
                }
                scribe.ExitNode();
            }
            else
            {
                Log.Message("Settings file NOT found.");
            }
        }

        public static void SaveSettings()
        {
            XMLScribe scribe = new XMLScribe(SettingsPath);
            scribe.mode = XMLScribe.ScribeMode.Saving;
            scribe.EnterNode("Settings");
            {
                XmlNode settingData = scribe.EnterNode("SettingsData");
                scribe.SaveValueIntoNode(data);
                scribe.ExitNode();
            }
            scribe.ExitNode();
            scribe.SaveDocument();
        }
    }
}
