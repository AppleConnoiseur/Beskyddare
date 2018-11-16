using Beskyddare.Localization;
using Beskyddare.UI;
using Beskyddare.Utility;
using Beskyddare.DataBinding;
using Godot;
using System;
using System.Collections.Generic;
using Beskyddare.XML;

namespace Beskyddare.Logic
{
    public class Game : Node
    {
        //For updating translations when language is changed.
        public List<Translator> translatorNodes = new List<Translator>();

        //For dealing with game logic
        public bool allowPlayerInput = true;
        public Faction playerFaction;
        public Faction enemyFaction;
        public Faction animalFaction;

        //Nodes
        public Node menuNode;
        public Node playNode;
        
        private static Game singleton;
        private static Node root;
        private static Map map;

        public static PackedScene mainMenuScene;
        public static PackedScene playScene;
        public static PackedScene settingsMenuScene;

        public Map CurrentMap
        {
            get
            {
                return map;
            }
        }

        public static Game Instance
        {
            get
            {
                return singleton;
            }
        }

        public static Node Root
        {
            get
            {
                return root;
            }
        }

        public override void _Ready()
        {
            //Setup Singleton
            singleton = this;

            //Set root node
            root = GetTree().CurrentScene;
            //Log.Message($"root={root.Name}");

            //Info
            Log.Message("Running directory: " + FileSystem.GameDirectory);

            //Load languages
            Log.Message("Loading languages from: " + LanguageDatabase.LanguageDirectory);
            LanguageDatabase.ReloadLanguages();
            foreach(Language language in LanguageDatabase.Languages)
            {
                Log.Message($"Found language: {language.name} with '{language.keyed.Count}' keyed strings.");
            }

            //Load data bindings
            DataBindingDatabase.ReloadDataBindings();

            Log.Message($"Type converters: {XMLTools.TypeConverters.ToString()}");

            //Load settings
            Settings.ReloadSettings();

            //Load scenes
            playScene = (PackedScene)ResourceLoader.Load("res://Scenes/Play.tscn");
            mainMenuScene = (PackedScene)ResourceLoader.Load("res://Scenes/MainMenu.tscn");
            settingsMenuScene = (PackedScene)ResourceLoader.Load("res://Scenes/SettingsMenu.tscn");

            //Setup factions
            playerFaction = new Faction()
            {
                name = "Player"
            };
            enemyFaction = new Faction()
            {
                name = "Enemy"
            };
            animalFaction = new Faction()
            {
                name = "Animal"
            };

            enemyFaction.AddEnemy(playerFaction);
            enemyFaction.AddEnemy(animalFaction);

            OpenMainMenu();
        }

        public void PreQuitGame()
        {
            Settings.SaveSettings();
        }

        public override void _ExitTree()
        {
            PreQuitGame();

            base._ExitTree();
        }

        public void ClearMenus()
        {
            if(Root != null && menuNode != null)
            {
                Root.RemoveChild(menuNode);
            }
            
        }

        public void PlaySceneInstanced(Map forMap)
        {
            Log.Message("Play scene instanced");
        }

        public void PlayGame()
        {
            ClearMenus();

            Node node = playScene.Instance();
            playNode = node;

            Game.map = new Map(node);
            node.Connect("tree_entered", this, "PlaySceneInstanced", Game.map.ToGDArray());

            Root.CallDeferred("add_child", node);
        }

        public void OpenMainMenu()
        {
            ClearMenus();

            Node node = mainMenuScene.Instance();
            menuNode = node;

            Root.CallDeferred("add_child", node);
        }

        public void OpenSettingsMenu()
        {
            ClearMenus();

            Node node = settingsMenuScene.Instance();
            menuNode = node;

            Root.CallDeferred("add_child", node);
        }

        public void OpenLoadGameMenu()
        {
            Log.Message("Open load game menu");
            /*ClearMenus();

            Node node = settingsMenuScene.Instance();
            menuNode = node;

            Root.CallDeferred("add_child", node);*/
        }
    }
}
