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
            Visibility = Visibility.Collapsed;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
            ActualPointerDirection = PointerDirection.None;

            Background = (Brush)Application.Current.Resources["SystemControlBackgroundChromeMediumBrush"];
            BorderBrush = (Brush)Application.Current.Resources["SystemControlForegroundBaseLowBrush"];
            Foreground = (Brush)Application.Current.Resources["SystemControlForegroundBaseHighBrush"];
            LightDismissOverlayBrush = new SolidColorBrush((Color)Application.Current.Resources["SystemAltMediumColor"]);
            BorderThickness = new Thickness(1);
            CornerRadius = new CornerRadius(4);
            Padding = new Thickness(10);
            BorderMargin = new Thickness(50);
            FontSize = 16;

            Content = new Grid
            {
                Children =
                {
                     new Rectangle()
                        .AssignTo(ref _overlay)
                        .Margin(0)
                        .Stretch()
                        .Collapsed()
                        .Bind(Rectangle.FillProperty, this, nameof(LightDismissOverlayBrush))
                        ,
                    new BubbleBorder()
                    {
                        
                        Content =  new ContentPresenter()
                            .AssignTo(ref _contentPresenter)
                            .Margin(0)
                            .Padding(0)
                            .TextWrapping(TextWrapping.WrapWholeWords)
                            .Bind(ContentPresenter.FontFamilyProperty, this, nameof(FontFamily))
                            .Bind(ContentPresenter.FontSizeProperty, this, nameof(FontSize))
                            .Bind(ContentPresenter.FontStretchProperty, this, nameof(FontStretch))
                            .Bind(ContentPresenter.FontStyleProperty, this, nameof(FontStyle))
                            .Bind(ContentPresenter.FontWeightProperty, this, nameof(FontWeight))
                            .Bind(ContentPresenter.ContentProperty, this, nameof(BorderContent))
                            .Bind(ContentPresenter.VisibilityProperty, this, nameof(BorderContent), convert: (object content)=> (content != null).ToVisibility())
                        
                    }
                        .AssignTo(ref _border)
                        .Bind(BubbleBorder.PaddingProperty, this, nameof(Padding))
                        .Bind(BubbleBorder.HorizontalContentAlignmentProperty, this, nameof(HorizontalContentAlignment))
                        .Bind(BubbleBorder.VerticalContentAlignmentProperty, this, nameof(VerticalContentAlignment))
                        .Bind(BubbleBorder.BackgroundProperty, this, nameof(Background))
                        .Bind(BubbleBorder.BorderBrushProperty, this, nameof(BorderBrush))
                        .Bind(BubbleBorder.CornerRadiusProperty, this, nameof(CornerRadius))
                        .Bind(BubbleBorder.HasShadowProperty, this, nameof(HasShadow))
                        .Bind(BubbleBorder.PointerBiasProperty, this, nameof(PointerBias))
                        .Bind(BubbleBorder.PointerCornerRadiusProperty, this, nameof(PointerCornerRadius))
                        .Bind(BubbleBorder.PointerLengthProperty, this, nameof(PointerLength))
                        .Bind(BubbleBorder.PointerTipRadiusProperty, this, nameof(PointerTipRadius))
                }
            }
                .AssignTo(ref _grid)
                .DataContext(this)
                .Margin(0)
                .Padding(0)
                .Stretch()
                ;


        }

    }
}
