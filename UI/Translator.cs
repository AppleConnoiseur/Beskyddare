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
    /// <summary>
    /// Translates the text of its parent node.
    /// </summary>
    public class Translator : Node
    {
        public bool alreadyTranslated = false;
        public string labelKey;

        public override void _EnterTree()
        {
            base._EnterTree();

            Game.Instance.translatorNodes.Add(this);
        }

        public override void _ExitTree()
        {
            base._ExitTree();

            Game.Instance.translatorNodes.Remove(this);
        }

        public override void _Ready()
        {
            DoTranslation();
        }

        public void DoTranslation()
        {
            //Translate label.
            {
                if (GetParent() is Label parent)
                {
                    if (!alreadyTranslated)
                    {
                        labelKey = parent.Text;
                        alreadyTranslated = true;
                    }

                    parent.Text = labelKey.Translate();
                    return;
                }
            }

            //Translate button.
            {
                if (GetParent() is Button parent)
                {
                    if (!alreadyTranslated)
                    {
                        labelKey = parent.Text;
                        alreadyTranslated = true;
                    }

                    parent.Text = labelKey.Translate();
                    return;
                }
            }
        }
    }
}
