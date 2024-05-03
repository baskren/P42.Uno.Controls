using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.UI.Xaml;

namespace P42.Uno.Controls.AnimateBar
{
    public partial class Right : Left
    {
        public Right()
        {
            //Height = 30;
            VerticalAlignment = VerticalAlignment.Stretch;
            Margin = new Thickness(0, 5, 0, 5);
            HorizontalAlignment = HorizontalAlignment.Right;

            dir = -1;
            //StaticRect.HorizontalAlignment = 
            DynamicRect.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Right;

        }
    }
}
