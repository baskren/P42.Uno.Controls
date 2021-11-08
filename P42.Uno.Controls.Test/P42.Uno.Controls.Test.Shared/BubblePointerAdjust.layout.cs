using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using P42.Uno.Controls;
using P42.Uno.Markup;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BubblePointerAdjustPage : Page
    {
        BubbleBorder _bubble;
        Slider _slider;
        Slider _borderThicknessSlider;

        void Build()
        {
            Content = new Grid()
                .Rows("*", 30, 30)
                .Children
                (
                    new BubbleBorder()
                        .Assign(out _bubble)
                        .Stretch()
                        .Background(Colors.Blue)
                        .BorderBrush(Colors.Green)
                        .BorderThickness(5)
                        .CornerRadius(5)
                        .PointerDown()
                        .PointerLength(20)
                        .PointerTipRadius(5)
                        .Content(new TextBlock { Text = "THIS IS THE CONTENT " }),
                    new Slider()
                        .Assign(out _slider)
                        .Row(1)
                        .MinMaxStep(0, 1, 0.001)
                        .Value(0.5)
                        .AddOnValueChanged(_slider_ValueChanged),
                    new Slider()
                        .Assign(out _borderThicknessSlider)
                        .Row(2)
                        .MinMaxStep(0, 20, 1)
                        .AddOnValueChanged(_borderThicknessSliderChanged)
                        .Value(1)
                );
        }
    }
}