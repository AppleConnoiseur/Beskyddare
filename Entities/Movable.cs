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
    public class Movable : KinematicBody2D, IMapInteractible
    {
        public Vector2 velocity;
        public Faction faction;

        public Faction Faction => faction;
        private Map map;
        private Navigation2D navigator;
        public bool hitObstacle = false;

        public Map Map
        {
            get
            {
                return map;
            }
        }

        public Navigation2D Navigator
        {
            get
            {
                return navigator;
            }
        }

        public override void _Ready()
        {
            base._Ready();
            navigator = this.GetChild<Navigation2D>();
        }

        public override void _EnterTree()
        {
            base._EnterTree();

            map = Game.Instance?.CurrentMap;
            Game.Instance?.CurrentMap.RegisterInteractible(this);
        }

        public override void _ExitTree()
        {
            base._ExitTree();

            Game.Instance?.CurrentMap.DeregisterInteractible(this);
        }

        public virtual void GetInput(float delta)
        {
            
        }

        public override void _PhysicsProcess(float delta)
        {
            GetInput(delta);
            Vector2 vel = MoveAndSlide(velocity);
            if(velocity != vel)
            {
                hitObstacle = true;
            }
            else
            {
                hitObstacle = false;
            }
        }

        public virtual void InteractibleRegistered(Map map)
        {
            
        }

        public virtual void InteractibleDeregistered(Map map)
        {
            
        }

        public virtual void SetupInteractible()
        {
            
        }
    }
}
