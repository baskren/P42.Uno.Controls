using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
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
                        .AddPointerEnteredHandler(Border_PointerEntered)
                        .AddPointerExitedHandler(Border_PointerExited)
                        .AddPointerPressedHandler(Border_PointerPressed)
                        .AddPointerReleasedHandler(Border_PointerReleased)
                        .AddPointerWheelChangedHandler(Border_PointerWheelChanged)
                        .AddPointerCanceledHandler(Border_PointerCanceled)
                        .AddPointerCaptureLostHandler(Border_PointerCaptureLost)
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
                        .AddPointerWheelChangedHandler(Border_PointerWheelChanged)
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
                        .AddPointerEnteredHandler(Border_PointerEntered)
                        .AddPointerExitedHandler(Border_PointerExited)
                        .AddPointerPressedHandler(Border_PointerPressed)
                        .AddPointerReleasedHandler(Border_PointerReleased)
                        .AddPointerWheelChangedHandler(Border_PointerWheelChanged)
                        .AddPointerCanceledHandler(Border_PointerCanceled)
                        .AddPointerCaptureLostHandler(Border_PointerCaptureLost)
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