using Beskyddare.Localization;
using Beskyddare.Logic;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.UI
{
    public class SettingsMenu : Node
    {
        public OptionButton languageOption;
        public Popup rebindActionPopup;

        public System.Collections.Generic.Dictionary<int, Language> languageBindings = new System.Collections.Generic.Dictionary<int, Language>();

        public override void _Ready()
        {
            //Get Popup.
            rebindActionPopup = GetNode<Popup>(new NodePath("Popup_RebindAction"));

            //Setup language selection.
            languageOption = GetNode<OptionButton>(new NodePath("CenterContainer/GridContainer/GridContainer/LanguageOption"));
            int activeID = -1;

            //Add valid languages.
            if(languageOption != null)
            {
                int index = 0;
                foreach (Language lang in LanguageDatabase.Languages)
                {
                    languageOption.AddItem(lang.name, index);
                    languageBindings[index] = lang;

                    if (lang == LanguageDatabase.Active)
                    {
                        activeID = index;
                    }

                    index++;
                }

                //Set current language.
                if(activeID != -1)
                {
                    languageOption.Select(activeID);
                }
            }
            else
            {
                GD.PrintErr("'languageOption' node is null in 'SettingsMenu' node!");
            }
        }

        public void LanguageSelected(int ID)
        {
            //Set to new language.
            LanguageDatabase.SetActiveLanguage(languageBindings[ID]);
        }

        public void GoBackToMainMenu()
        {
            Game.Instance.OpenMainMenu();
        }
    }
}
