using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace P42.Uno.Controls
{
    public partial class ContentAndDetailPresenter : Grid
    {
        TargetedPopup _targetedPopup;
        Border _detailDrawer;
        Rectangle _overlay;

        ColumnDefinition _drawerColumnDefinition = new ColumnDefinition { Width = new GridLength(0) };
        RowDefinition _drawerRowDefinition = new RowDefinition { Height = new GridLength(0) };

        const double popupMargin = 30;

        void Build()
        {
            // r0,c0 : Content
            // r1,c0 : Footer
            this.Rows("*", "auto");
            this.Columns("*");
            _detailDrawer = new Border()
                .BorderBrush(SystemColors.BaseMediumHigh)
                .Background(SystemColors.AltHigh)
                .BorderThickness(1);

            _overlay = new Rectangle()
                .Row(0)
                .RowSpan(2)
                .Bind(Rectangle.FillProperty, this, nameof(LightDismissOverlayBrush));

            _targetedPopup = new TargetedPopup
            {
                IsLightDismissEnabled = false,
                LightDismissOverlayMode = LightDismissOverlayMode.Off,
                IsAnimated = false
            }
                .Bind(TargetedPopup.TargetProperty, this, nameof(Target))
                .Padding(4)
                .Opacity(0)
                .Margin(popupMargin)
                .HorizontalContentAlignment(HorizontalAlignment.Stretch)
                .VerticalContentAlignment(VerticalAlignment.Stretch)
                .PreferredPointerDirection(PointerDirection.Vertical)
                .FallbackPointerDirection(PointerDirection.Any)
                .Bind(TargetedPopup.WidthProperty, this, nameof(PopupWidth))
                .Bind(TargetedPopup.HeightProperty, this, nameof(PopupHeight))
                ;

            LightDismissOverlayBrush = SystemColors.AltMedium.WithAlpha(0.25).ToBrush();
            _overlay.PointerPressed += OnDismissPointerPressed;

            _targetedPopup.Popped += OnTargetedPopupPopped;
#if __ANDROID__
            _targetedPopup.IsLightDismissEnabled = true;
#endif
        }
    }
}
