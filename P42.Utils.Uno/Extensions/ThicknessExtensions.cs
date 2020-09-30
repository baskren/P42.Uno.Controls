using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace P42.Utils.Uno
{
    public static class ThicknessExtensions
    {
        public static double Horizontal(this Thickness thickness)
            => thickness.Left + thickness.Right;

        public static double Vertical(this Thickness thickness)
            => thickness.Top + thickness.Bottom;

        public static double Average(this Thickness thickness)
            => (thickness.Horizontal() + thickness.Vertical()) / 4.0;
    }
}
