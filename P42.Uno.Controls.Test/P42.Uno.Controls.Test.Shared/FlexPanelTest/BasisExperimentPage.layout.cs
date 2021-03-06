using P42.Uno.Controls;
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
using P42.Uno.Markup;
using P42.Utils.Uno;
using P42.Utils;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlexPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class BasisExperimentPage : Page
    {
        ToggleSwitch autoSwitch2, relativeSwitch2, autoSwitch4, relativeSwitch4;
        Slider slider2, slider4;
        FlexPanel _flexLayout;
        Border label2, label4;

        static BooleanInverseConverter BooleanInverseConverter = new BooleanInverseConverter();
        static Microsoft.Toolkit.Uwp.UI.Converters.StringFormatConverter StringFormatConverter = new Microsoft.Toolkit.Uwp.UI.Converters.StringFormatConverter();

        void Build()
        {

            Content = new Grid()
                .Margin(10, 0)
                .Rows("auto", "*")
                .Children
                (
                    new Grid()
                        .Row(0)
                        .Rows("auto", "auto", "auto", "auto", "auto", "auto", "auto")
                        .Columns("auot", "*", "*")
                        /*
                        .Resources(new ResourceDictionary()
                            .FluintAdd("TextBlockStyle", new Style(typeof(TextBlock)).Add(VerticalAlignmentProperty, VerticalAlignment.Center))
                            .FluintAdd("StackPanelStyle", new Style(typeof(StackPanel))
                                .Add(StackPanel.OrientationProperty, Orientation.Horizontal)
                                .Add(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Center)
                            )
                        */
                        .AddStyle(typeof(TextBlock), (TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center))
                        .AddStyle(typeof(StackPanel),
                            (StackPanel.OrientationProperty, Orientation.Horizontal),
                            (StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Center))
                        .Children
                        (
                            // TextBlock 2 controls
                            new TextBlock()
                                .RowCol(0, 0)
                                .Text("TextBlock 2:"),
                            new StackPanel()
                                .RowCol(0, 1)
                                .Children
                                (
                                    new TextBlock().Text("Auto"),
                                    new ToggleSwitch()
                                        .Assign(out autoSwitch2)
                                        .On()
                                        .AddOnToggled(OnLabel2AutoSwitchToggled)
                                ),
                            new StackPanel()
                                .RowCol(0, 2)
                                .Children
                                (
                                    new TextBlock().Text("Is Relative"),
                                    new ToggleSwitch()
                                        .Assign(out relativeSwitch2)
                                        .On()
                                        .AddOnToggled(OnLabel2IsRelativeSwitchToggled)
                                        .Bind(ToggleSwitch.IsEnabledProperty, autoSwitch2, nameof(ToggleSwitch.IsOn), BindingMode.TwoWay, BooleanInverseConverter)
                                ),
                            new Slider()
                                .Assign(out slider2)
                                .AddOnValueChanged(OnLabel2SliderValueChanged)
                                .RowCol(1, 1).ColumnSpan(2)
                                .MinMaxStep(0, 1, 0.01)
                                .Bind(Slider.IsEnabledProperty, autoSwitch2, nameof(ToggleSwitch.IsOn), BindingMode.TwoWay, BooleanInverseConverter),
                            new TextBlock()
                                .RowCol(2, 1).ColumnSpan(2)
                                .CenterHorizontal()
                                .Bind(TextBlock.TextProperty, slider2, nameof(Slider.Value), BindingMode.OneWay, StringFormatConverter, "Value = {0:F2}"),


                            // TextBlock 4 controls
                            new TextBlock()
                                .RowCol(3, 0)
                                .Text("TextBlock 4:"),
                            new StackPanel()
                                .RowCol(3, 1)
                                .Children
                                (
                                    new TextBlock().Text("Auto"),
                                    new ToggleSwitch()
                                        .Assign(out autoSwitch4)
                                        .On()
                                        .AddOnToggled(OnLabel4AutoSwitchToggled)
                                ),
                            new StackPanel()
                                .RowCol(0, 2)
                                .Children
                                (
                                    new TextBlock().Text("Is Relative"),
                                    new ToggleSwitch()
                                        .Assign(out relativeSwitch4)
                                        .On()
                                        .AddOnToggled(OnLabel4IsRelativeSwitchToggled)
                                        .Bind(ToggleSwitch.IsEnabledProperty, autoSwitch4, nameof(ToggleSwitch.IsOn), BindingMode.TwoWay, BooleanInverseConverter)
                                ),
                            new Slider()
                                .Assign(out slider4)
                                .AddOnValueChanged(OnLabel2SliderValueChanged)
                                .RowCol(4, 1).ColumnSpan(2)
                                .MinMaxStep(0, 1, 0.01)
                                .Bind(Slider.IsEnabledProperty, autoSwitch4, nameof(ToggleSwitch.IsOn), BindingMode.TwoWay, BooleanInverseConverter),
                            new TextBlock()
                                .RowCol(5, 1).ColumnSpan(2)
                                .CenterHorizontal()
                                .Bind(TextBlock.TextProperty, slider4, nameof(Slider.Value), BindingMode.OneWay, StringFormatConverter, "Value = {0:F2}"),

                            new TextBlock()
                                .RowCol(6, 0).Text("Orientation"),
                            new EnumPicker()
                            {
                                EnumType = typeof(FlexDirection),
                            }
                                .RowCol(6, 1)
                                .StretchHorizontal()
                                .ContentLeft()
                        ),
                    new FlexPanel
                    {
                        Direction = FlexDirection.Row,
                        Wrap = FlexWrap.Wrap
                    }
                        .Assign(out _flexLayout)
                        .Row(1)
                        .Background(Colors.AliceBlue)
                        .Children
                        (
                            new Border().Background(Colors.Pink)
                                .Child(
                                    new TextBlock { Text = "TextBlock 1" }.Foreground(Colors.Blue)
                                ),
                            new Border().Background(Colors.Cyan).FlexPanelBasis(50)
                                .Assign(out label2)
                                .Child(
                                    new TextBlock { Text = "TextBlock 2" }.Foreground(Colors.Red)
                                ),
                            new Border().Background(Colors.Pink)
                                .Child(
                                    new TextBlock { Text = "TextBlock 3" }.Foreground(Colors.Blue)
                                ),
                            new Border().Background(Colors.Cyan).FlexPanelBasis(50)
                                .Assign(out label4)
                                .Child(
                                    new TextBlock { Text = "TextBlock 4" }.Foreground(Colors.Red)
                                ),
                            new Border().Background(Colors.Pink)
                                .Child(
                                    new TextBlock { Text = "TextBlock 5" }.Foreground(Colors.Blue)
                                )
                        )
                );
        }
    }
}