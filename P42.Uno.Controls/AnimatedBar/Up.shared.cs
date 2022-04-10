using System;
using System.Collections.Generic;
using System.Text;

namespace P42.Uno.Controls.AnimateBar
{
    public partial class Up : Down
    {
        public Up()
        {
            Width = 30;
            Margin = new Windows.UI.Xaml.Thickness(5, 0, 5, 0);
            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;

            dir = -1;
            //StaticRect.VerticalAlignment = 
            DynamicRect.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
        }
    }
}
