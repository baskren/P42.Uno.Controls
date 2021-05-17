using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace P42.Uno.Controls
{
    public partial class SegmentedControl : ContentControl
    {
        SegmentPanel _panel;

        void Build()
        {
            Content = new SegmentPanel()
                    .Assign(out _panel)
                    .Margin(0)
                    .Stretch()
                    .Bind(SegmentPanel.OrientationProperty, this, nameof(Orientation));

            HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            VerticalContentAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
            Padding = new Windows.UI.Xaml.Thickness(-1);
        }
    }
}
