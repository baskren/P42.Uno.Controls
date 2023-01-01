using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BubblePointerAdjustPage : Page
    {
        public BubblePointerAdjustPage()
        {
            //this.InitializeComponent();
            Build();
        }

        private void _slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            _bubble.PointerAxialPosition = _slider.Value;
        }

        private void _borderThicknessSliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            _bubble.BorderThickness = new Thickness( _borderThicknessSlider.Value);
        }

    }
}
