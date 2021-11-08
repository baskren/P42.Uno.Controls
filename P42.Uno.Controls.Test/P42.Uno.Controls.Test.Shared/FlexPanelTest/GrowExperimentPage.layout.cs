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
using P42.Utils;
using P42.Utils.Uno;
using P42.Uno.Markup;
using P42.Uno.Controls;
using Windows.UI;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlexPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class GrowExperimentPage : Page
    {
        static Microsoft.Toolkit.Uwp.UI.Converters.StringFormatConverter stringFormatConverter = new Microsoft.Toolkit.Uwp.UI.Converters.StringFormatConverter();

        Slider grow2Slider, grow4Slider;

        void Build()
        {
            Content = new Grid()
                .Margin(10, 0)
                .Rows("auto", "*")
                .Children
                (
                    new Grid()
                        .Rows("auto", "auto", "auto", "auto")
                        .Columns("auto", "*")
                        .AddStyle(typeof(TextBlock), (TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center))
                        .Children
                        (
                            new TextBlock()
                                .RowCol(0, 0)
                                .Text("TextBlock 2:"),
                            new Slider()
                                .Assign(out grow2Slider)
                                .RowCol(0, 1)
                                .MinMaxStep(0, 5, 0.01),
                            new TextBlock()
                                .RowCol(1, 1)
                                .CenterHorizontal()
                                .Bind(TextBlock.TextProperty, grow2Slider, nameof(Slider.Value), BindingMode.OneWay, stringFormatConverter, "Grow = {0:F2}"),

                            new TextBlock()
                                .RowCol(2, 0)
                                .Text("TextBlock 4:"),
                            new Slider()
                                .Assign(out grow4Slider)
                                .RowCol(2, 1)
                                .MinMaxStep(0, 5, 0.01),
                            new TextBlock()
                                .RowCol(3, 1)
                                .CenterHorizontal()
                                .Bind(TextBlock.TextProperty, grow4Slider, nameof(Slider.Value), BindingMode.OneWay, stringFormatConverter, "Grow = {0:F2}"),

                            new FlexPanel()
                                .RowCol(1, 0)
                                .Direction(FlexDirection.Row)
                                .Background(Colors.AliceBlue)
                                .AddStyle(typeof(TextBlock), (TextBlock.FontSizeProperty, 30))
                                .Children
                                (
                                    new Border()
                                        .Background(Colors.Pink)
                                        .FlexPanelBasis(50),
                                    new Border()
                                        .Background(Colors.Cyan)
                                        .Bind(FlexPanel.GrowProperty, grow2Slider, nameof(Slider.Value), BindingMode.TwoWay)
                                        .Child(new TextBlock().Text("TextBlock 2").Foreground(Colors.Red)),
                                    new Border()
                                        .Background(Colors.Pink)
                                        .FlexPanelBasis(50),
                                    new Border()
                                        .Background(Colors.Cyan)
                                        .Bind(FlexPanel.GrowProperty, grow4Slider, nameof(Slider.Value), BindingMode.TwoWay)
                                        .Child(new TextBlock().Text("TextBlock 4").Foreground(Colors.Red)),
                                    new Border()
                                        .Background(Colors.Pink)
                                        .FlexPanelBasis(50)
                                )
                        )
                );
        }
    }
}