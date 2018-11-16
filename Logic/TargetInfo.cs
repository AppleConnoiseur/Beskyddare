using Beskyddare.Entities;
using Beskyddare.XML;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Logic
{
    public class TargetInfo : IExposeData
    {
        private Vector2 targetLocation;
        private Movable targetMovable;

        public Vector2 Location
        {
            get
            {
                if(targetMovable != null)
                {
                    return targetMovable.GlobalPosition;
                }
                else
                {
                    return targetLocation;
                }
            }
        }

        public Movable Target
        {
            get
            {
                return targetMovable;
            }
        }

        public TargetInfo()
        {
            targetLocation = new Vector2();
            targetMovable = null;
        }

        public TargetInfo(Vector2 location)
        {
            targetLocation = location;
            targetMovable = null;
        }

        public TargetInfo(Movable target)
        {
            targetLocation = new Vector2();
            targetMovable = target;
        }

        public void ExposeData(XMLScribe scribe)
        {
            if(scribe.mode == XMLScribe.ScribeMode.Saving)
            {
                if (targetMovable != null)
                {
                    scribe.LookupValue(ref targetMovable, "targetMovable");
                }
                else
                {
                    scribe.LookupValue(ref targetLocation, "targetLocation");
                }
            }
            else if (scribe.mode == XMLScribe.ScribeMode.Loading)
            {
                scribe.LookupValue(ref targetMovable, "targetMovable");
                scribe.LookupValue(ref targetLocation, "targetLocation");
            }
        }

        public static implicit operator TargetInfo(Vector2 vector)
        {
            return new TargetInfo(vector);
        }

        public static implicit operator TargetInfo(Movable movable)
        {
            return new TargetInfo(movable);
        }

        public static implicit operator Vector2(TargetInfo targetInfo)
        {
            return targetInfo.Location;
        }

        public static implicit operator Movable(TargetInfo targetInfo)
        {
            return targetInfo.Target;
        }
    }
}
