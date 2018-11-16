using Beskyddare.Entities;
using Beskyddare.Logic;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.AI
{
    public class BaseAI : Node
    {
        public AIController Controller
        {
            get
            {
                return GetParent() as AIController;
            }
        }

        public Movable Locomotion
        {
            get
            {
                return Controller.MovingController;
            }
        }

        public Map Map
        {
            get
            {
                return Locomotion.Map;
            }
        }

        public Player Player
        {
            get
            {
                return Map.player;
            }
        }

        public virtual void TickAI(float delta)
        {

        }

        public virtual void StopLocomotion()
        {
            Locomotion.velocity = new Vector2();
        }
    }
}
