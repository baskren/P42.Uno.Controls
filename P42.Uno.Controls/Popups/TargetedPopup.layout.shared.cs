﻿using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using P42.Uno.Markup;
using P42.Utils.Uno;
#if NETFX_CORE
using Popup = Windows.UI.Xaml.Controls.Primitives.Popup;
#else
using Popup = Windows.UI.Xaml.Controls.Popup;
#endif

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
        protected Popup _popup;
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

            Content = new Popup
            {
                Child =
                
                    new BubbleBorder()
                    {
                        Content = new ContentPresenter()
                            .Assign(out _contentPresenter)
                            .Margin(0)
                            .Padding(0)
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
                        .Bind(BubbleBorder.PointerTipRadiusProperty, this, nameof(PointerTipRadius))
                
            }
                .Assign(out _popup)
                .DataContext(this)
                .Margin(0)
                //.Padding(0)
                .Stretch()
                .Bind(Popup.IsLightDismissEnabledProperty, this, nameof(IsLightDismissEnabled))
                .Bind(Popup.LightDismissOverlayModeProperty, this, nameof(LightDismissOverlayMode))
                ;

            //this.PointerMoved += OnPointerMoved;
            //this.PointerEntered += OnPointerEntered;
            _popup.Opened += OnPopupOpened;
            _popup.Closed += OnPopupClosed;
        }

    }
}