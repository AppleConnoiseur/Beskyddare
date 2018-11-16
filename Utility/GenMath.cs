using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Utility
{
    public static class GenMath
    {
        public static float SqDistance(Vector2 a, Vector2 b)
        {
            return a.DistanceSquaredTo(b);
        }

        public static Vector2 OffsetInDirectionVector(Vector2 position, Vector2 direction, float distance)
        {
            Vector2 result = position + (direction.Normalized() * distance);
            return result;
        }

        public static Vector2 RelativeTo(this Vector2 from, Vector2 to)
        {
            return from - to;
        }
    }
}
