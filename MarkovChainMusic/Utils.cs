using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkovChainMusic
{
    static class Utils
    {
        internal static int[] Range(int min, int max)
        {
            List<int> ints = new List<int>();
            for (int i = min; i < max; i++)
                ints.Add(i);
            return ints.ToArray();
        }
    }
}
