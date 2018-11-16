using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Debug
{
    public interface DebugPainter
    {
        void PaintDebug(DebugDrawer drawer);
        bool ShouldRedraw(DebugDrawer drawer);
    }
}
