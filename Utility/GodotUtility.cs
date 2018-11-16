using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Utility
{
    public static class GodotUtility
    {
        public static Godot.Array ToGDArray(this object[] array)
        {
            Godot.Array gdArray = new Godot.Array();
            foreach(object obj in array)
            {
                gdArray.Add(obj);
            }
            return gdArray;
        }

        public static Godot.Array ToGDArray(this object obj)
        {
            Godot.Array gdArray = new Godot.Array();
            gdArray.Add(obj);
            return gdArray;
        }

        public static Godot.Array MakeGDArray(params object[] array)
        {
            Godot.Array gdArray = new Godot.Array();
            foreach (object obj in array)
            {
                gdArray.Add(obj);
            }
            return gdArray;
        }

        public static IEnumerable<Node> AllChildren(this Node start)
        {
            foreach (Node child in start.GetChildren())
            {
                yield return child;
                foreach(Node subChild in AllChildren(child))
                {
                    yield return subChild;
                }
            }
        }

        public static T GetChild<T>(this Node start) where T : Node
        {
            foreach(Node child in start.GetChildren())
            {
                if(child is T)
                {
                    return (T)child;
                }
            }

            return null;
        }
    }
}
