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
    public static class GenReachability
    {
        public static bool CanReach(this Movable seeker, TargetInfo targetInfo)
        {
            if(seeker.Navigator is Navigation2D navigator)
            {
                object simplePath = navigator.GetSimplePath(seeker.GlobalPosition, targetInfo) != null;
                return simplePath != null;
            }
            return false;
        }
    }
}
