using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beskyddare.Utility;
using Beskyddare.Logic;

namespace Beskyddare.Utility
{
    public static class GenPhysics
    {
        public static Vector2 SimpleRayCast(CanvasItem spaceNode, Vector2 from, Vector2 to, out bool success)
        {
            Physics2DDirectSpaceState physicsState = Physics2DServer.SpaceGetDirectState(spaceNode.GetWorld2d().Space);
            Dictionary result = physicsState.IntersectRay(from, to, GodotUtility.MakeGDArray(spaceNode));

            if(result.ContainsKey("position"))
            {
                success = true;
                return (Vector2)result["position"];
            }
            else
            {
                success = false;
                return new Vector2();
            }
        }

        public static RayCastHitInfo RayCastObject(CanvasItem spaceNode, Vector2 from, Vector2 to, out bool success)
        {
            Physics2DDirectSpaceState physicsState = Physics2DServer.SpaceGetDirectState(spaceNode.GetWorld2d().Space);
            Dictionary result = physicsState.IntersectRay(from, to, GodotUtility.MakeGDArray(spaceNode));

            if (result.ContainsKey("position"))
            {
                success = true;
                RayCastHitInfo info = new RayCastHitInfo();
                info.position = (Vector2)result["position"];
                if (result.ContainsKey("collider"))
                {
                    info.collider = (Godot.Object)result["collider"];
                }
                return info;
            }
            else
            {
                success = false;
                return null;
            }
        }
    }
}
