using System;
using System.Collections.Generic;
using System.Text;

namespace Bc3
{
    public static class Global
    {
        public static int DebugDepth { get; set; }

        public static string DebugIndent(int index = 0)
        {
            if (index < 0)
                DebugDepth += index;
            var result =  new string('\t', DebugDepth);
            if (index > 0)
                DebugDepth += index;
            return result;
        }
    }
}
