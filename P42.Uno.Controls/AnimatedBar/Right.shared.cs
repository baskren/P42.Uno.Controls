using System;
using System.Collections.Generic;
using System.Text;

namespace P42.Uno.Controls.AnimateBar
{
    public partial class Right : Left
    {
        public Right()
        {
            dir = -1;
            //StaticRect.HorizontalAlignment = 
            DynamicRect.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;

        }
    }
}
