using P42.Uno.Markup;
using P42.Utils.Uno;
using P42.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using Microsoft.UI.Xaml.Documents;

namespace P42.Uno.Controls
{
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
                    .Bind<Rectangle, Orientation, Visibility>(Rectangle.VisibilityProperty, this, nameof(Orientation), convert:VerticalVisible) 
                    .Fill(new LinearGradientBrush
                    {
                        StartPoint = new Windows.Foundation.Point(0.5, 0),
                        EndPoint = new Windows.Foundation.Point(0.5, 1),
                        GradientStops =
                        {
                            new GradientStop{ Offset=0.0, Color=P42.Uno.Markup.ColorExtensions.ColorFromHex("#8222")},
                            new GradientStop{ Offset=1.0, Color=P42.Uno.Markup.ColorExtensions.ColorFromHex("#0222")},
                        }
                    }),
                new Rectangle()  // footer shadow
                    .Height(5)
                    .Stretch().Bottom()
                    .Bind<Rectangle, Orientation, Visibility>(Rectangle.VisibilityProperty, this, nameof(Orientation), convert: VerticalVisible)
                    .Fill(new LinearGradientBrush
                    {
                        StartPoint = new Windows.Foundation.Point(0.5, 0),
                        EndPoint = new Windows.Foundation.Point(0.5, 1),
                        GradientStops =
                        {
                            new GradientStop{ Offset=0.0, Color=P42.Uno.Markup.ColorExtensions.ColorFromHex("#0222")},
                            new GradientStop{ Offset=1.0, Color=P42.Uno.Markup.ColorExtensions.ColorFromHex("#8222")},
                        }
                    }),

                new Rectangle()  // left shadow
                    .Width(5)
                    .Stretch().Left()
                    .Bind<Rectangle, Orientation, Visibility>(Rectangle.VisibilityProperty, this, nameof(Orientation), convert: HorizontalVisible)
                    .Fill(new LinearGradientBrush
                    {
                        StartPoint = new Windows.Foundation.Point(0.5, 0),
                        EndPoint = new Windows.Foundation.Point(0.5, 1),
                        GradientStops =
                        {
                            new GradientStop{ Offset=0.0, Color=P42.Uno.Markup.ColorExtensions.ColorFromHex("#8222")},
                            new GradientStop{ Offset=1.0, Color=P42.Uno.Markup.ColorExtensions.ColorFromHex("#0222")},
                        }
                    }),
                new Rectangle()  // right shadow
                    .Height(5)
                    .Stretch().Right()
                    .Bind<Rectangle, Orientation, Visibility>(Rectangle.VisibilityProperty, this, nameof(Orientation), convert: HorizontalVisible)
                    .Fill(new LinearGradientBrush
                    {
                        StartPoint = new Windows.Foundation.Point(0.5, 0),
                        EndPoint = new Windows.Foundation.Point(0.5, 1),
                        GradientStops =
                        {
                            new GradientStop{ Offset=0.0, Color=P42.Uno.Markup.ColorExtensions.ColorFromHex("#0222")},
                            new GradientStop{ Offset=1.0, Color=P42.Uno.Markup.ColorExtensions.ColorFromHex("#8222")},
                        }
                    })

            );
        }

        private Visibility VerticalVisible(Orientation orientation)
            => orientation == Orientation.Vertical ? Visibility.Visible : Visibility.Collapsed;
        
        private Visibility HorizontalVisible(Orientation orientation)
            => orientation == Orientation.Horizontal ? Visibility.Visible : Visibility.Collapsed;

        #endregion
    }
}