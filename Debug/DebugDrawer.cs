using Beskyddare.Utility;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Debug
{
    public class DebugDrawer : Node2D
    {
        public bool shouldRedraw = false;

        private List<DebugPainter> painters = new List<DebugPainter>();

        public override void _Ready()
        {
            //Add all painters we can.
            foreach(Node child in GodotUtility.AllChildren(GetParent()))
            {
                if(child is DebugPainter painter)
                {
                    AddPainter(painter);
                }
            }
        }

        public void AddPainter(DebugPainter painter)
        {
            painters.Add(painter);
            shouldRedraw = true;
        }

        public override void _Process(float delta)
        {
            foreach (DebugPainter painter in painters)
            {
                if(painter.ShouldRedraw(this))
                {
                    shouldRedraw = true;
                    break;
                }
            }

            if (shouldRedraw)
            {
                Update();
                shouldRedraw = false;
            }
        }

        public override void _Draw()
        {
            foreach(DebugPainter painter in painters)
            {
                painter.PaintDebug(this);
            }
        }
    }
}
