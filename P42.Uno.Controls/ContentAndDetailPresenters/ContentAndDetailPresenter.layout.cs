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
    public partial class ContentAndDetailPresenter : Panel
    {
        //ContentPresenter _contentPresenter;
        //ContentPresenter _detailContentPresenter;
        //ContentPresenter _footerContentPresenter;
        TargetedPopup _targetedPopup;
        Border _detailDrawer;
        Rectangle _overlay;

        void Build()
        {
            this.Children(

                new Border()
                    .Assign(out _detailDrawer)
                    .BorderBrush(SystemColors.BaseMediumHigh)
                    .Background(SystemColors.AltHigh)
                    .BorderThickness(1),

                new Rectangle()
                    .Assign(out _overlay)
                    .Collapsed()
                    .Bind(Rectangle.FillProperty, this, nameof(LightDismissOverlayBrush))
            );

            //_detailContentPresenter = new ContentPresenter();

            _targetedPopup = new TargetedPopup()
                .Padding(4)
                .LightDismissOverlayBrush("#01FFFFFF")
                .Opacity(0)
                .Margin(30)
                .PreferredPointerDirection(PointerDirection.Vertical);

        }
    }
}
