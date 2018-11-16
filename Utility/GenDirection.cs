using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Utility
{
    public static class GenDirection
    {
        private static Vector2[] cardinal;
        private static Vector2[] eightWay;

        public static Vector2[] Cardinal
        {
            get
            {
                return cardinal;
            }
        }

        public static Vector2[] EightWay
        {
            get
            {
                return eightWay;
            }
        }

        static GenDirection()
        {
            cardinal = new Vector2[]
            {
                //           x,  y
                new Vector2( 0, -1),
                new Vector2( 1,  0),
                new Vector2( 0,  1),
                new Vector2(-1,  0)
            };

            eightWay = new Vector2[]
            {
                //           x,  y
                new Vector2( 0, -1),
                new Vector2( 1, -1),
                new Vector2( 1,  0),
                new Vector2( 1,  1),
                new Vector2( 0,  1),
                new Vector2(-1,  1),
                new Vector2(-1,  0),
                new Vector2(-1, -1)
            };
        }
    }
}
