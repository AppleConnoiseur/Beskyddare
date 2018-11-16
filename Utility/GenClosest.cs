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
    public static class GenClosest
    {
        public static Movable ClosestMovable(this Movable seeker, float maxDistance = float.MaxValue, Predicate<Movable> candidateValidator = null, List<Movable> candidates = null)
        {
            //No validator means a default validator is used.
            if (candidateValidator == null)
            {
                candidateValidator = mov => true;
            }

            //No candidates? Fill up the list with Movables nearby.
            if (candidates == null)
            {
                candidates = new List<Movable>();
                Map map = seeker.Map;
                foreach(IMapInteractible interactible in map.interactibles)
                {
                    if(interactible is Movable movable && candidateValidator(movable) && seeker.GlobalPosition.DistanceTo(movable.GlobalPosition) <= maxDistance)
                    {
                        candidates.Add(movable);
                    }
                }
            }

            Movable closest = candidates.OrderBy(order => seeker.GlobalPosition.DistanceTo(order.GlobalPosition)).First();

            return closest;
        }

        public static Movable ClosestReachableMovable(this Movable seeker, float maxDistance = float.MaxValue, Predicate<Movable> candidateValidator = null, List<Movable> candidates = null)
        {
            Predicate<Movable> predicate = null;
            if(candidateValidator == null)
            {
                predicate = mov => seeker.CanReach(mov);
            }
            else
            {
                predicate = mov => seeker.CanReach(mov) && candidateValidator(mov);
            }

            return ClosestMovable(seeker, maxDistance, predicate, candidates);
        }
    }
}
