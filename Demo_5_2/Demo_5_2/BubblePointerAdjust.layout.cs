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
using P42.Uno.Controls;
using P42.Uno.Markup;
using Microsoft.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace P42.Uno.Controls.Demo;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
[Microsoft.UI.Xaml.Data.Bindable]
public sealed partial class BubblePointerAdjustPage : Page
{
    BubbleBorder _bubble = new();
    Slider _slider = new();
    Slider _borderThicknessSlider = new();

    void Build()
    {
        Content = new Grid()
            .Rows("*", 30, 30)
            .Children
            (
                _bubble
                    .Stretch()
                    .Background(Colors.Blue)
                    .BorderBrush(Colors.Green)
                    .BorderThickness(5)
                    .CornerRadius(5)
                    .PointerDown()
                    .PointerLength(20)
                    .PointerTipRadius(5)
                    .Content(new TextBlock { Text = "THIS IS THE CONTENT " }),
                _slider
                    .Row(1)
                    .MinMaxStep(0, 1, 0.001)
                    .Value(0.5)
                    .AddValueChangedHandler(_slider_ValueChanged),
                _borderThicknessSlider
                    .Row(2)
                    .MinMaxStep(0, 20, 1)
                    .AddValueChangedHandler(_borderThicknessSliderChanged)
                    .Value(1)
            );
    }
}
