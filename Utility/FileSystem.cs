using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Utility
{
    public static class FileSystem
    {
        public static string GameDirectory
        {
            get
            {
                return ProjectSettings.GlobalizePath("res://");
            }
        }

        public static string DataDirectory
        {
            get
            {
                return GameDirectory + "Data";
            }
        }

        public static string FixPath(this string path)
        {
            return path.Replace('\\', '/');
        }
    }
}
