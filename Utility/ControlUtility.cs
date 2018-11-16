using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Utility
{
    public static class ControlUtility
    {
        public static Control MakeHorizontalFiller()
        {
            Control control = new Control();
            control.SizeFlagsHorizontal = (int)Control.SizeFlags.ExpandFill;
            return control;
        }

        public static Control MakeVerticalFiller()
        {
            Control control = new Control();
            control.SizeFlagsVertical = (int)Control.SizeFlags.ExpandFill;
            return control;
        }
    }
}
