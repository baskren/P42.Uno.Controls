using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using P42.Uno.Markup;
using P42.Utils.Uno;
using Microsoft.UI.Xaml.Input;
using System;

namespace P42.Uno.Controls
{
    public partial class TargetedPopup : UserControl
    {
        #region Defaults
        const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;
        const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Center;
        const double DefaultBorderThickness = 1;
        const double DefaultCornerRadius = 4;
        #endregion

        #region Visual Elements
        //internal BubbleBorder _border;
        internal BubbleBorder _border;
        internal Bubble _shadow;
        Grid _grid;
        #endregion

        void Build()
        {
            Margin = new Thickness(40, 40, 40, 40);
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
            ActualPointerDirection = PointerDirection.None;

            Background = (Brush)Application.Current.Resources["SystemControlBackgroundChromeMediumBrush"];
            BorderBrush = (Brush)Application.Current.Resources["SystemControlForegroundBaseLowBrush"];
            Foreground = (Brush)Application.Current.Resources["SystemControlForegroundBaseHighBrush"];
            BorderThickness = new Thickness(DefaultBorderThickness);
            CornerRadius = new CornerRadius(DefaultCornerRadius);
            FontSize = 16;

            Content = new Grid
            {
                Children =
                {
                    new BubbleBorder()
                        .Assign(out _border)
                        .RowCol(1,1)
                        .Bind(BubbleBorder.OpacityProperty, this, nameof(Opacity))
                        .Bind(BubbleBorder.HorizontalContentAlignmentProperty, this, nameof(HorizontalContentAlignment))
                        .Bind(BubbleBorder.VerticalContentAlignmentProperty, this, nameof(VerticalContentAlignment))
                        .Bind(BubbleBorder.WidthProperty, this, nameof(Width))
                        .Bind(BubbleBorder.HeightProperty, this, nameof(Height))
                        .Bind(BubbleBorder.PaddingProperty, this, nameof(Padding))
                        .Bind(BubbleBorder.BackgroundProperty, this, nameof(Background))
                        .Bind(BubbleBorder.BorderBrushProperty, this, nameof(BorderBrush))
                        .Bind(BubbleBorder.BorderThicknessProperty, this, nameof(BorderThickness))
                        .Bind(BubbleBorder.CornerRadiusProperty, this, nameof(CornerRadius))
                        //.Bind(BubbleBorder.HasShadowProperty, this, nameof(HasShadow))
                        .Bind(BubbleBorder.PointerBiasProperty, this, nameof(PointerBias))
                        .Bind(BubbleBorder.PointerCornerRadiusProperty, this, nameof(PointerCornerRadius))
                        .Bind(BubbleBorder.PointerLengthProperty, this, nameof(PointerLength))
                        .Bind(BubbleBorder.PointerTipRadiusProperty, this, nameof(PointerTipRadius))
                }
            }
                .Assign(out _grid)
                .Rows(Margin.Left, "*", Margin.Right)
                .Columns(Margin.Top,"*", Margin.Bottom)
                .DataContext(this)
                .Margin(0)
                .Padding(0)
                .Stretch()
                .Bind(Grid.BackgroundProperty, this, nameof(PageOverlayBrush))
                .AddTapHandler(OnBackgroundTapped)
                ;

        }

        async void OnBackgroundTapped(object sender, TappedRoutedEventArgs e)
        {
            if (CancelOnPageOverlayTouch)
                await PopAsync(PopupPoppedCause.BackgroundTouch, false, e);
        }


    }
}
