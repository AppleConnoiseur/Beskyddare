using Beskyddare.Utility;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.UI
{
    public class ControlMapImporter : Node
    {
        public override void _Ready()
        {
            foreach (object item in InputMap.GetActions().Where(binding => binding is string bindingString && bindingString.StartsWith("hero")))
            {
                //Log.Message("item=" + item + "; item.Type=" + item.GetType().FullName);
                string actionName = item as string;

                MakeInputMapper(actionName);

                /*foreach (object actionItem in InputMap.GetActionList(actionName))
                {
                    InputEvent inputAction = actionItem as InputEvent;
                    if (inputAction != null)
                    {
                        Log.Message($"name={inputAction.AsText()}");
                    }
                }*/
            }
        }

        public void MakeInputMapper(string actionName)
        {
            HBoxContainer container = new HBoxContainer();

            //Label
            Label actionLabel = new Label();
            actionLabel.Valign = Label.VAlign.Center;
            actionLabel.Text = "Controls." + actionName;
            actionLabel.AddChild(new Translator());
            container.AddChild(actionLabel);

            container.AddChild(ControlUtility.MakeHorizontalFiller());

            //Bound actions
            Godot.Array actionList = InputMap.GetActionList(actionName);

            foreach (object actionItem in actionList)
            {
                InputEvent inputAction = actionItem as InputEvent;
                MakeRebindActionButton(container, inputAction);
            }

            //Add new binding options if its not 2 there.
            if(actionList.Count < 2)
            {
                int difference = 2 - actionList.Count;
                for(int i = 0; i < difference; i++)
                {
                    MakeRebindActionButton(container, null);
                }
            }

            GetParent().CallDeferred("add_child", container);
        }

        public void MakeRebindActionButton(Container parentContainer, InputEvent inputAction)
        {
            Button button = new Button();

            if (inputAction != null)
            {
                //Keyboard
                if (inputAction is InputEventKey inputKey)
                {
                    KeyList key = (KeyList)inputKey.Scancode;
                    button.Text = key.ToString();
                }

                //Mouse
                if (inputAction is InputEventMouseButton inputMouseButton)
                {
                    button.Text = "Controls.Mouse." + inputMouseButton.ButtonIndex;
                    button.AddChild(new Translator());
                }

                //Controller
                if (inputAction is InputEventJoypadButton inputJoypadButton)
                {
                    button.Text = "Controls.Mouse." + inputJoypadButton.ButtonIndex;
                    button.AddChild(new Translator());
                }
            }
            else
            {
                button.Text = "Controls.RebindThis";
                button.AddChild(new Translator());
            }

            parentContainer.AddChild(button);
        }

        public void RebindAction(Control source)
        {

        }
    }
}
