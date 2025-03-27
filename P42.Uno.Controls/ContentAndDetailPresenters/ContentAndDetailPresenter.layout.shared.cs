using P42.Uno.Markup;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;

namespace P42.Uno.Controls;

public partial class ContentAndDetailPresenter : Grid
{
    private TargetedPopup _targetedPopup;
    private Border _detailDrawer;
    private Rectangle _overlay;

    private ColumnDefinition _drawerColumnDefinition = new ColumnDefinition().Auto();
    private RowDefinition _drawerRowDefinition = new RowDefinition().Auto();

    private const double popupMargin = 30;

    private void Build()
    {
        // r0,c0 : Content
        // r1,c0 : Footer
        this.Rows("*", _drawerRowDefinition);
        this.Columns("*", _drawerColumnDefinition);
        _overlay = new Rectangle()
            .Row(0)
            .RowSpan(2)
            .WBind(Shape.FillProperty, this, PageOverlayBrushProperty)
            .WBind(IsHitTestVisibleProperty, this, IsPageOverlayHitTestVisibleProperty)
            .WBind(VisibilityProperty, this, PageOverlayBrushProperty, converter: VisibilityConverter.Instance)
            .AddTappedHandler(OnDismissPointerPressed);

        _detailDrawer = new Border()
            .WBind(Border.BorderBrushProperty, this, DetailBorderColorProperty, converter: SolidBrushConverter.Instance)
            .WBind(Border.BorderThicknessProperty, this, BorderThicknessProperty)
            .WBind(BackgroundProperty, this, DetailBackgroundColorProperty, converter: SolidBrushConverter.Instance);

        _targetedPopup = new TargetedPopup()
                .Padding(0)
                .Opacity(0)
                .Margin(popupMargin)
                .ContentStretch()
                .HasShadow()
                .PreferredPointerDirection(PointerDirection.Up)
                .FallbackPointerDirection(PointerDirection.Any)
                .PageOverlay(Colors.Transparent)
                .PageOverlayHitTestVisible(false)
                .WBind(TargetedPopup.WeakTargetProperty, this, WeakTargetProperty)
                .WBind(TargetedPopup.BorderColorProperty, this, DetailBorderColorProperty)
                .WBind(TargetedPopup.BackgroundColorProperty, this, DetailBackgroundColorProperty)
                .WBind(TargetedPopup.BorderWidthProperty, this, PopupBorderWidthProperty)
                .WBind(TargetedPopup.CornerRadiusProperty, this, DetailCornerRadiusProperty)
                .WBind(MinHeightProperty, this, PopupMinHeightProperty)
                .WBind(MinWidthProperty, this, PopupMinWidthProperty)
                .WBind(TargetedPopup.PageOverlayBrushProperty, this, PageOverlayBrushProperty)
                .WBind(TargetedPopup.IsPageOverlayHitTestVisibleProperty, this, IsPageOverlayHitTestVisibleProperty)
                .WBind(TargetedPopup.PopOnPageOverlayTouchProperty, this, PopOnPageOverlayTouchProperty)
                .WBind(TargetedPopup.HorizontalAlignmentProperty, this, PopupHorizontalAlignmentProperty)
                .WBind(TargetedPopup.VerticalAlignmentProperty, this, PopupVerticalAlignmentProperty)
                .AddPoppedHandler(OnTargetedPopupPopped)
            ;

        PageOverlayBrush = Colors.Black.WithAlpha(0.01).ToBrush();
            

    }


}
