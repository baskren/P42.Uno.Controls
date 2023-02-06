using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using P42.Uno.Markup;
using P42.Utils.Uno;

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
        internal BubbleBorder _border;
        protected ContentPresenter _contentPresenter;
        protected Microsoft.UI.Xaml.Controls.Primitives.Popup _popup;
        #endregion

        void Build()
        {
            //Visibility = Visibility.Collapsed;
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

            Content = new Microsoft.UI.Xaml.Controls.Primitives.Popup
            {
                Child =
                    new BubbleBorder()
                    {
                        Content = new ContentPresenter()
                            .Assign(out _contentPresenter)
                            .Margin(0)
                            .Padding(Padding)
                            .Stretch()
                            .TextWrapping(TextWrapping.WrapWholeWords)
                            .BindFont(this)
                            .BindNullCollapse()
                    }
                        .Assign(out _border)
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
                        .Bind(BubbleBorder.PointerTipRadiusProperty, this, nameof(PointerTipRadius)),
            }
                .Assign(out _popup)
                .DataContext(this)
                .Margin(0)
                //.Padding(0)
                .Stretch()
                .Bind(Microsoft.UI.Xaml.Controls.Primitives.Popup.IsLightDismissEnabledProperty, this, nameof(IsLightDismissEnabled))
                .Bind(Microsoft.UI.Xaml.Controls.Primitives.Popup.LightDismissOverlayModeProperty, this, nameof(LightDismissOverlayMode))
                ;

            //this.PointerMoved += OnPointerMoved;
            //this.PointerEntered += OnPointerEntered;
            _popup.Opened += OnPopupOpened;
            _popup.Closed += OnPopupClosed;

            LightDismissOverlayMode = LightDismissOverlayMode.On;
            /*
#if __IOS__
            var uiFont = UIKit.UIFont.SystemFontOfSize(12f);
            var family = uiFont.FamilyName;
            this.FontFamily(family);
#endif
            */

            _contentPresenter.SizeChanged += _contentPresenter_SizeChanged;
        }

        private void _contentPresenter_SizeChanged(object sender, SizeChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine($" ---- TargetedPopup.ContentPresenter_SizeChanged [{args.PreviousSize}] -> [{args.NewSize}] : [{_contentPresenter.DesiredSize}]---- ");

        }
    }
}
