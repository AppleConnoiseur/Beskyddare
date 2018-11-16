using Beskyddare.Entities;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.AI
{
    /// <summary>
    /// Controls a entity through AI.
    /// </summary>
    public class AIController : Node
    {
        //Bindingable properties
        public float searchRadius;
        public float movementSpeed;
        public bool allowRandomWander;
        public float wanderDistance;
        public float idleTime;
        public float chaseTime;

        //Properties
        public Movable MovingController
        {
            get
            {
                return GetParent() as Movable;
            }
        }

        public BaseAI AI
        {
            get
            {
                return GetChild(0) as BaseAI;
            }
        }

        //Functions
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            AI.TickAI(delta);
        }
    }
}
