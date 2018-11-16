using Beskyddare.Utility;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Logic
{
    public class GameLoop : MainLoop
    {
        public override void _Finalize()
        {
            Log.Message("Game is quitting!");

            base._Finalize();
        }
    }
}
