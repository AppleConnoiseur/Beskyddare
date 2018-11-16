using Beskyddare.Logic;
using Beskyddare.Utility;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Entities
{
    public class Player : Bipedal
    {
        public override void SetupInteractible()
        {
            base.SetupInteractible();

            faction = Game.Instance.playerFaction;
        }

        public override void InteractibleRegistered(Map map)
        {
            base.InteractibleRegistered(map);
            map.player = this;
            Log.Message("Player registered to map.");
        }

        public override void GetInput(float delta)
        {
            if(!Game.Instance.allowPlayerInput)
            {
                return;
            }

            directionVector = new Vector2();

            if (Input.IsActionPressed("hero_move_up"))
            {
                directionVector.y = -1f;
            }
            else if (Input.IsActionPressed("hero_move_down"))
            {
                directionVector.y = 1f;
            }

            if (Input.IsActionPressed("hero_move_left"))
            {
                directionVector.x = -1f;
            }
            else if (Input.IsActionPressed("hero_move_right"))
            {
                directionVector.x = 1f;
            }

            velocity = directionVector.Normalized() * 200f;
        }
    }
}
