using P42.Uno.Controls;
using P42.Uno.Markup;
using P42.Utils.Uno;
using P42.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlexPanelTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class ExperimentPage : Page
    {
        Stepper numberStepper;
        FlexPanel flexPanel;

        static Microsoft.Toolkit.Uwp.UI.EnumValuesExtension EnumValuesExtension = new Microsoft.Toolkit.Uwp.UI.EnumValuesExtension();

        void Build()
        {
            Content = new Grid()
                .Margin(10, 0)
                .Rows("auto", "*")
                .Children
                (
                    new Grid()
                        .Rows("auto", "auto", "auto", "auto", "auto", "auto")
                        .Columns("auto","*")
                        .AddStyle(typeof(TextBox), (TextBox.VerticalAlignmentProperty, VerticalAlignment.Center))
                        .Children
                        (
                            new TextBlock()
                                .RowCol(0,0)
                                .CenterVertical()
                                .Text("Number of items:"),
                            new StackPanel()
                                .RowCol(0,1)
                                .Horizontal()
                                .Spacing(10)
                                .Children
                                (
                                    new Stepper()
                                    {
                                        Increment = 1,
                                        MaxValue = 99,
                                        MinValue = 0,
                                        Value = 3
                                    }
                                        .Assign(out numberStepper)
                                ),
                            new TextBlock()
                                .RowCol(1,0)
                                .CenterVertical()
                                .Text("Direction:"),
                            new ComboBox()
                                .RowCol(1,1)
                                .StretchHorizontal()
                                .ContentLeft()
                                .ItemsSource(Enum.GetValues(typeof(FlexDirection)).Cast<FlexDirection>())
                                .Bind(ComboBox.SelectedItemProperty, flexPanel, nameof(FlexPanel.Direction), BindingMode.TwoWay),
                            new TextBlock()
                                .RowCol(2,0)
                                .CenterVertical()
                                .Text("Wrap:"),
                            new ComboBox()
                                .RowCol(2, 1)
                                .StretchHorizontal()
                                .ContentLeft()
                                .ItemsSource(Enum.GetValues(typeof(FlexWrap)).Cast<FlexWrap>())
                                .Bind(ComboBox.SelectedItemProperty, flexPanel, nameof(FlexPanel.Wrap), BindingMode.TwoWay),
                            new TextBlock()
                                .RowCol(3, 0)
                                .CenterVertical()
                                .Text("JustifyContent:"),
                            new ComboBox()
                                .RowCol(3, 1)
                                .StretchHorizontal()
                                .ContentLeft()
                                .ItemsSource(Enum.GetValues(typeof(FlexJustify)).Cast<FlexJustify>())
                                .Bind(ComboBox.SelectedItemProperty, flexPanel, nameof(FlexPanel.JustifyContent), BindingMode.TwoWay),
                            new TextBlock()
                                .RowCol(4, 0)
                                .CenterVertical()
                                .Text("AlignItems:"),
                            new ComboBox()
                                .RowCol(4, 1)
                                .StretchHorizontal()
                                .ContentLeft()
                                .ItemsSource(Enum.GetValues(typeof(FlexAlignItems)).Cast<FlexAlignItems>())
                                .Bind(ComboBox.SelectedItemProperty, flexPanel, nameof(FlexPanel.AlignItems), BindingMode.TwoWay),
                            new TextBlock()
                                .RowCol(5, 0)
                                .CenterVertical()
                                .Text("AlignContent:"),
                            new ComboBox()
                                .RowCol(5, 1)
                                .StretchHorizontal()
                                .ContentLeft()
                                .ItemsSource(Enum.GetValues(typeof(FlexAlignContent)).Cast<FlexAlignContent>())
                                .Bind(ComboBox.SelectedItemProperty, flexPanel, nameof(FlexPanel.AlignContent), BindingMode.TwoWay)
                        ),
                    new FlexPanel()
                        .Assign(out flexPanel)
                        .Row(1)
                        .Background(Colors.AliceBlue)
                );

            numberStepper.ValueChanged += OnNumberStepperValueChanged;
        }
    }
}