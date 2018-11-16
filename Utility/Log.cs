using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Utility
{
    public class Log
    {
        public static void Message(string message)
        {
            GD.Print(message);
        }

        public static void Warning(string message)
        {
            GD.Print("Warning: " + message);
        }
        public static void Error(string message)
        {
            GD.PrintErr("Error: " + message);
            GD.Print(message);
        }
    }
}
