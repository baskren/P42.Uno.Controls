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
using P42.Utils;
using P42.Utils.Uno;
using P42.Uno.Markup;
using P42.Uno.Controls;
using Windows.UI;
using Microsoft.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlexPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShrinkExperimentPage : Page
    {
        Slider shrink2Slider, shrink4Slider;
        Microsoft.Toolkit.Uwp.UI.Converters.StringFormatConverter StringFormatConverter = new Microsoft.Toolkit.Uwp.UI.Converters.StringFormatConverter();

        void Build()
        {
            Content = new Grid()
                .Margin(10, 0)
                .Rows("auto", "*")
                .Children
                (
                    new Grid()
                        .Rows("auto", "auto", "auto", "auto")
                        .Columns("auto","*")
                        .AddStyle(typeof(TextBlock), (TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center))
                        .Children
                        (
                            new TextBlock().RowCol(0,0).Text("TextBlock 2:"),
                            new Slider()
                                .Assign(out shrink2Slider)
                                .RowCol(0,1)
                                .MinMaxStep(0,5,0.01)
                                .TickFrequency(1)
                                .Value(1),
                            new TextBlock()
                                .RowCol(1,1)
                                .CenterHorizontal()
                                .Bind(TextBlock.TextProperty, shrink2Slider, nameof(Slider.Value), BindingMode.OneWay, StringFormatConverter, "Shrink = {0:F2}"),

                            new TextBlock().RowCol(2, 0).Text("TextBlock 4:"),
                            new Slider()
                                .Assign(out shrink4Slider)
                                .RowCol(2, 1)
                                .MinMaxStep(0, 5, 0.01)
                                .TickFrequency(1)
                                .Value(1),
                            new TextBlock()
                                .RowCol(3, 1)
                                .CenterHorizontal()
                                .Bind(TextBlock.TextProperty, shrink4Slider, nameof(Slider.Value), BindingMode.OneWay, StringFormatConverter, "Shrink = {0:F2}")
                        ),
                    new FlexPanel()
                        .Row(1)
                        .Background(Colors.AliceBlue)
#if NET6_0_WINDOWS10_0_19041_0

                        .AddStyle(typeof(TextBlock),(TextBlock.FontSizeProperty, 40))
#else
                        .AddStyle(typeof(TextBlock),(TextBlock.FontSizeProperty, 32))
#endif
                        .Children
                        (
                            new Border()
                                .Background(Colors.Pink)
                                .Child(new TextBlock().Text("TextBlock 1").Foreground(Colors.Blue)),
                            new Border()
                                .Background(Colors.Cyan)
                                .Bind(FlexPanel.ShrinkProperty, shrink2Slider, nameof(Slider.Value), BindingMode.TwoWay)
                                .Child(new TextBlock().Text("TextBlock 2").Foreground(Colors.Red)),
                            new Border()
                                .Background(Colors.Pink)
                                .Child(new TextBlock().Text("TextBlock 3").Foreground(Colors.Blue)),
                            new Border()
                                .Background(Colors.Cyan)
                                .Bind(FlexPanel.ShrinkProperty, shrink4Slider, nameof(Slider.Value), BindingMode.TwoWay)
                                .Child(new TextBlock().Text("TextBlock 4").Foreground(Colors.Red)),
                            new Border()
                                .Background(Colors.Pink)
                                .Child(new TextBlock().Text("TextBlock 5").Foreground(Colors.Blue))
                        )
                );
        }
    }
}