using Microsoft.Toolkit.Uwp.UI.Animations;
using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Uno.Extensions;
using Uno.Extensions.ValueType;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;
using P42.Uno.Markup;

namespace P42.Uno.Controls
{
    public partial class TargetedPopup : UserControl
    {
        protected Rectangle _overlay;
        protected BubbleBorder _border;
        protected ContentPresenter _contentPresenter;
        protected Grid _grid;

        void Build()
        {
            _contentPresenter = new ContentPresenter()
                .Margin(0)
                .Padding(0)
                .TextWrapping(TextWrapping.WrapWholeWords)
                //.Bind(ContentPresenter.FontFamilyProperty, FontFamily)
                //.Bind(ContentPresenter.FontSizeProperty, FontSize)
                //.Bind(ContentPresenter.FontStretchProperty, FontStretch)
                //.Bind(ContentPresenter.FontStyleProperty, FontStyle)
                //.Bind(ContentPresenter.FontWeightProperty, FontWeight)
                //.Bind(ContentPresenter.VisibilityProperty, BubbleContent, convert: (object content)=> (content != null).ToVisibility());
                ;

            _border = new BubbleBorder()
                /*
                .Bind(BubbleBorder.PaddingProperty, Padding)
                .Bind(BubbleBorder.MarginProperty, Margin)
                .Bind(BubbleBorder.HorizontalContentAlignmentProperty, HorizontalContentAlignment)
                .Bind(BubbleBorder.VerticalContentAlignmentProperty, VerticalContentAlignment)
                .Bind(BubbleBorder.BackgroundProperty, Background)
                .Bind(BubbleBorder.BorderBrushProperty, BorderBrush)
                .Bind(BubbleBorder.CornerRadiusProperty, CornerRadius)
                .Bind(BubbleBorder.HasShadowProperty, HasShadow)
                .Bind(BubbleBorder.PointerBiasProperty, PointerBias)
                .Bind(BubbleBorder.PointerCornerRadiusProperty, PointerCornerRadius)
                .Bind(BubbleBorder.PointerLengthProperty, PointerLength)
                .Bind(BubbleBorder.PointerTipRadiusProperty, PointerTipRadius)
                */
                .Content(_contentPresenter);

            _overlay = new Rectangle()
                .Margin(0)
                .Stretch()
                .Opacity(1.0)
                .Collapsed()
                //.Bind(Rectangle.FillProperty, LightDismissOverlayBrush);
                ;


            _grid = new Grid
            {
                Children =
                {
                    _overlay,
                    _border,
                }
            }
                .Margin(0)
                .Padding(0)
                .Stretch()
                //.Background((Brush)Application.Current.Resources["SystemControlBackgroundChromeMediumBrush"])
                //.BorderBrush((Brush)Application.Current.Resources["SystemControlForegroundBaseLowBrush"]);
                ;

            Content = _grid;
        }


    }
}
