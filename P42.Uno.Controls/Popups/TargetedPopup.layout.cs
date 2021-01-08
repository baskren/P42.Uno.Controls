using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using P42.Uno.Markup;
using P42.Utils.Uno;

namespace P42.Uno.Controls
{
    public partial class TargetedPopup : UserControl
    {
        #region Defaults
        const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;
        const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Center;
        const double DefaultPopupMargin = 50;
        const double DefaultPopupPadding = 10;
        const double DefaultBorderThickness = 1;
        const double DefaultCornerRadius = 4;
        #endregion

        #region Visual Elements
        protected Rectangle _overlay;
        protected BubbleBorder _border;
        protected ContentPresenter _contentPresenter;
        protected Grid _grid;
        #endregion

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
            BorderThickness = new Thickness(DefaultBorderThickness);
            CornerRadius = new CornerRadius(DefaultCornerRadius);
            FontSize = 16;

            Content = new Grid
            {
                Children =
                {
                     new Rectangle()
                        .Assign(out _overlay)
                        .Margin(0)
                        .Stretch()
                        .Collapsed()
                        .Bind(Rectangle.FillProperty, this, nameof(LightDismissOverlayBrush))
                        ,
                    new BubbleBorder()
                    {
                        
                        Content =  new ContentPresenter()
                            .Assign(out _contentPresenter)
                            .Margin(0)
                            .Padding(0)
                            .TextWrapping(TextWrapping.WrapWholeWords)
                            .Bind(ContentPresenter.FontFamilyProperty, this, nameof(FontFamily))
                            .Bind(ContentPresenter.FontSizeProperty, this, nameof(FontSize))
                            .Bind(ContentPresenter.FontStretchProperty, this, nameof(FontStretch))
                            .Bind(ContentPresenter.FontStyleProperty, this, nameof(FontStyle))
                            .Bind(ContentPresenter.FontWeightProperty, this, nameof(FontWeight))
                            .Bind(ContentPresenter.ContentProperty, this, nameof(PopupContent))
                            .BindNullCollapse()
                    }
                        .Assign(out _border)
                        .Bind(BubbleBorder.HorizontalContentAlignmentProperty, this, nameof(HorizontalContentAlignment))
                        .Bind(BubbleBorder.VerticalContentAlignmentProperty, this, nameof(VerticalContentAlignment))
                        .Bind(BubbleBorder.PaddingProperty, this, nameof(PopupPadding))
                        .Bind(BubbleBorder.BackgroundProperty, this, nameof(Background))
                        .Bind(BubbleBorder.BorderBrushProperty, this, nameof(BorderBrush))
                        .Bind(BubbleBorder.BorderThicknessProperty, this, nameof(BorderThickness))
                        .Bind(BubbleBorder.CornerRadiusProperty, this, nameof(CornerRadius))
                        .Bind(BubbleBorder.HasShadowProperty, this, nameof(HasShadow))
                        .Bind(BubbleBorder.PointerBiasProperty, this, nameof(PointerBias))
                        .Bind(BubbleBorder.PointerCornerRadiusProperty, this, nameof(PointerCornerRadius))
                        .Bind(BubbleBorder.PointerLengthProperty, this, nameof(PointerLength))
                        .Bind(BubbleBorder.PointerTipRadiusProperty, this, nameof(PointerTipRadius))
                }
            }
                .Assign(out _grid)
                .DataContext(this)
                .Margin(0)
                .Padding(0)
                .Stretch()
                ;


        }

    }
}
