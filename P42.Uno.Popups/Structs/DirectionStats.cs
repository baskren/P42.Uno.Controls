using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace P42.Uno.Popups
{
    struct DirectionStats

    {
        public Size FreeSpace;
        public double MinFree => Math.Min(FreeSpace.Width, FreeSpace.Height);
        public PointerDirection PointerDirection;
        public Size BorderSize;

        public override string ToString()
        {
            return "{PointerDirection=" + PointerDirection + ", BorderSize=" + BorderSize + ", FreeSpace=" + FreeSpace + "}";
        }
    }
}
