using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
    partial class SimpleCell
#if __ANDROID__
        : TextBlock
#endif
    {
        public SimpleCell()
        {
#if __ANDROID__
            HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Height = 40;
            DataContextChanged += (s, e) =>
            {
                Text = DataContext.ToString();
            };
#endif
        }
    }
}
