using System;
using System.Collections.Generic;
using System.Text;

namespace P42.Uno.Controls.AnimateBar
{
    public partial class Up : Down
    {
        public Up()
        {
            dir = -1;
            //StaticRect.VerticalAlignment = 
            DynamicRect.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
        }
    }
}
