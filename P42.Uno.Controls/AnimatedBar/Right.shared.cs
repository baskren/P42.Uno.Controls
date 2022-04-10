using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace P42.Uno.Controls.AnimateBar
{
    public partial class Right : Left
    {
        public Right()
        {
            Height = 30;
            Margin = new Thickness(0, 5, 0, 5);
            HorizontalAlignment = HorizontalAlignment.Right;

            dir = -1;
            //StaticRect.HorizontalAlignment = 
            DynamicRect.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;

        }
    }
}
