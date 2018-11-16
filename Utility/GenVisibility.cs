using Beskyddare.Entities;
using Beskyddare.Logic;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Utility
{
    public static class GenVisibility
    {
        public static bool CanSeeTarget(this Movable seer, Movable target)
        {
            RayCastHitInfo hitInfo = GenPhysics.RayCastObject(seer, seer.GlobalPosition, target.GlobalPosition, out bool hitSomething);
            if(!hitSomething || hitInfo.collider == null)
            {
                return false;
            }

            Node hitNode = hitInfo.collider as Node;
            return hitNode == target;
        }

        public static bool CanSeeTarget(this Movable seer, Movable target, float maxDistance)
        {
            RayCastHitInfo hitInfo = GenPhysics.RayCastObject(seer, seer.GlobalPosition, target.GlobalPosition, out bool hitSomething);
            if (!hitSomething || hitInfo.collider == null)
            {
                return false;
            }

            Node hitNode = hitInfo.collider as Node;
            return hitNode == target && seer.GlobalPosition.DistanceTo(target.GlobalPosition) <= maxDistance;
        }
    }
}
