using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Utility
{
    public static class GenRandom
    {
        public static Random random;

        static GenRandom()
        {
            random = new Random(20081107);
        }

        public static bool Bool()
        {
            return random.NextDouble() > 0.5d;
        }

        public static int Integer(int size)
        {
            return (int)(random.NextDouble() * (double)size);
        }
    }
}
