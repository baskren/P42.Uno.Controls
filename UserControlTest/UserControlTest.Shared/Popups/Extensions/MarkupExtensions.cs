using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace UserControlTest.Popups
{
    public static class MarkupExtensions
    {
        public static bool Negate(bool value)
            => !value;

    }
}
