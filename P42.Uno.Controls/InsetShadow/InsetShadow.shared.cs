using P42.Uno.Markup;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Xaml.Media;

namespace P42.Uno.Controls;

[Microsoft.UI.Xaml.Data.Bindable]
public partial class InsetShadow : Grid
{
    #region Properties

    #region Orientation Property
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
        nameof(Orientation),
        typeof(Orientation),
        typeof(InsetShadow),
        new PropertyMetadata(Orientation.Vertical)
    );
    public Orientation Orientation
    {
        get => (Orientation)GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }
    #endregion Orientation Property

    #endregion


    #region Construction
    public InsetShadow() 
    {
        this.Children
        (
            new Rectangle()  // header shadow
                .Height(5)
                .Stretch().Top()
                .WBind<Rectangle, Orientation, Visibility>(VisibilityProperty, this, OrientationProperty, convert:VerticalVisible) 
                .Fill(new LinearGradientBrush
                {
                    StartPoint = new Windows.Foundation.Point(0.5, 0),
                    EndPoint = new Windows.Foundation.Point(0.5, 1),
                    GradientStops =
                    {
                        new GradientStop{ Offset=0.0, Color=ColorExtensions.ColorFromHex("#8222")},
                        new GradientStop{ Offset=1.0, Color=ColorExtensions.ColorFromHex("#0222")},
                    }
                }),
            new Rectangle()  // footer shadow
                .Height(5)
                .Stretch().Bottom()
                .WBind<Rectangle, Orientation, Visibility>(VisibilityProperty, this, OrientationProperty, convert: VerticalVisible)
                .Fill(new LinearGradientBrush
                {
                    StartPoint = new Windows.Foundation.Point(0.5, 0),
                    EndPoint = new Windows.Foundation.Point(0.5, 1),
                    GradientStops =
                    {
                        new GradientStop{ Offset=0.0, Color=ColorExtensions.ColorFromHex("#0222")},
                        new GradientStop{ Offset=1.0, Color=ColorExtensions.ColorFromHex("#8222")},
                    }
                }),
                
            new Rectangle()  // left shadow
                .Width(5)
                .Stretch().Left()
                .WBind<Rectangle, Orientation, Visibility>(VisibilityProperty, this, OrientationProperty, convert: HorizontalVisible)
                .Fill(new LinearGradientBrush
                {
                    StartPoint = new Windows.Foundation.Point(0, 0.5),
                    EndPoint = new Windows.Foundation.Point(1, 0.5),
                    GradientStops =
                    {
                        new GradientStop{ Offset=0.0, Color=ColorExtensions.ColorFromHex("#8222")},
                        new GradientStop{ Offset=1.0, Color=ColorExtensions.ColorFromHex("#0222")},
                    }
                }),
            //.Fill(Microsoft.UI.Colors.Pink),
            new Rectangle()  // right shadow
                .Width(5)
                .Stretch().Right()
                .WBind<Rectangle, Orientation, Visibility>(VisibilityProperty, this, OrientationProperty, convert: HorizontalVisible)                    
                .Fill(new LinearGradientBrush
                {
                    StartPoint = new Windows.Foundation.Point(0, 0.5),
                    EndPoint = new Windows.Foundation.Point(1, 0.5),
                    GradientStops =
                    {
                        new GradientStop{ Offset=0.0, Color=ColorExtensions.ColorFromHex("#0222")},
                        new GradientStop{ Offset=1.0, Color=ColorExtensions.ColorFromHex("#8222")},
                    }
                })
            //.Fill(Microsoft.UI.Colors.Pink)

        );
    }

    private Visibility VerticalVisible(Orientation orientation)
        => orientation == Orientation.Vertical ? Visibility.Visible : Visibility.Collapsed;

    private Visibility HorizontalVisible(Orientation orientation)
    { 
        var result = orientation == Orientation.Horizontal ? Visibility.Visible : Visibility.Collapsed;
        return result;
    }

    #endregion
}
