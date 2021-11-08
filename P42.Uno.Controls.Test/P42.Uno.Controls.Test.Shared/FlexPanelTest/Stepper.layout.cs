using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using P42.Uno.Controls;
using P42.Uno.Markup;
using P42.Utils.Uno;
using P42.Utils;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace FlexPanelTest
{
    public partial class Stepper : UserControl
    {
        Border _leftSegment, _centerSegment, _rightSegment;
        TextBlock _valueTextBox;

        void Build()
        {
            Content = new Grid()
                .Height(30)
                .Columns(30, "*", 30)
                .Children
                (
                    new Border()
                        .Assign(out _leftSegment)
                        .CornerRadius(4,0,0,4)
                        .Background(SystemColors.BaseLow)
                        .BorderBrush(SystemColors.BaseMediumHigh)
                        .BorderThickness(0)
                        .AddOnPointerEntered(Border_PointerEntered)
                        .AddOnPointerExited(Border_PointerExited)
                        .AddOnPointerPressed(Border_PointerPressed)
                        .AddOnPointerReleased(Border_PointerReleased)
                        .AddOnPointerWheelChanged(Border_PointerWheelChanged)
                        .AddOnPointerCanceled(Border_PointerCanceled)
                        .AddOnPointerCaptureLost(Border_PointerCaptureLost)
                        .Child
                        (
                            new TextBlock().Text("-")
                                .Foreground((Brush)Resources["ButtonForegroundThemeBrush"])
                                .Center()
                                .FontSize(20)
                                .Padding(0)
                        ),
                    new Border()
                        .Assign(out _centerSegment)
                        .RowCol(0,1)
                        .Background(SystemColors.BaseLow)
                        .BorderBrush(SystemColors.BaseMediumHigh)
                        .AddOnPointerWheelChanged(Border_PointerWheelChanged)
                        .BorderThickness(0)
                        .Child
                        (
                            new TextBlock()
                                .Assign(out _valueTextBox)
                                .Foreground((Brush)Resources["ButtonForegroundThemeBrush"])
                                .Text("0")
                                .Center()
                                .CenterTextAlignment()
                                .Padding(0)
                                .Margin(5,0)
                        ),
                    new Border()
                        .Assign(out _rightSegment)
                        .RowCol(0,2)
                        .CornerRadius(4, 0, 0, 4)
                        .Background(SystemColors.BaseLow)
                        .BorderBrush(SystemColors.BaseMediumHigh)
                        .BorderThickness(0)
                        .AddOnPointerEntered(Border_PointerEntered)
                        .AddOnPointerExited(Border_PointerExited)
                        .AddOnPointerPressed(Border_PointerPressed)
                        .AddOnPointerReleased(Border_PointerReleased)
                        .AddOnPointerWheelChanged(Border_PointerWheelChanged)
                        .AddOnPointerCanceled(Border_PointerCanceled)
                        .AddOnPointerCaptureLost(Border_PointerCaptureLost)
                        .Child
                        (
                            new TextBlock().Text("+")
                                .Foreground((Brush)Resources["ButtonForegroundThemeBrush"])
                                .Center()
                                .FontSize(20)
                                .Padding(0)
                        )
                );
                
        }
    }
}