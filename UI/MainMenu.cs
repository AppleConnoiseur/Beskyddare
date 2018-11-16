using Beskyddare.Logic;
using Beskyddare.Utility;
using Godot;
using System;

namespace Beskyddare.UI
{
    public class MainMenu : Node
    {
        public override void _Ready()
        {
            
        }

        public void StartGame()
        {
            Game.Instance.PlayGame();
        }

        public void OpenLoadGameMenu()
        {
            Log.Message("Open load game menu");
        }

        public void OpenSettings()
        {
            Game.Instance.OpenSettingsMenu();
        }

        public void QuitGame()
        {
            GetTree().Quit();
        }
    }
}
