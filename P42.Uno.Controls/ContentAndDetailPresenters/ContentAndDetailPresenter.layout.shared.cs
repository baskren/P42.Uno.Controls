namespace P42.Uno.Controls;

public partial class ContentAndDetailPresenter : Grid
{
    private TargetedPopup _targetedPopup = new TargetedPopup();
    private Border _detailDrawer = new Border();
    private Rectangle _overlay = new Rectangle();

    private ColumnDefinition _drawerColumnDefinition = new ColumnDefinition().Auto();
    private RowDefinition _drawerRowDefinition = new RowDefinition().Auto();

    private const double popupMargin = 30;

    private void Build()
    {
        // r0,c0 : Content
        // r1,c0 : Footer
        this.Rows("*", _drawerRowDefinition);
        this.Columns("*", _drawerColumnDefinition);
        _overlay 
            .Row(0)
            .RowSpan(2)
            .AltBind(Shape.FillProperty, this, PageOverlayBrushProperty)
            .AltBind(IsHitTestVisibleProperty, this, IsPageOverlayHitTestVisibleProperty)
            .AltBind(VisibilityProperty, this, PageOverlayBrushProperty, converter: VisibilityConverter.Instance)
            .AddTappedHandler(OnDismissPointerPressed);

        _detailDrawer 
            .AltBind(Border.BorderBrushProperty, this, DetailBorderColorProperty, converter: SolidBrushConverter.Instance)
            .AltBind(Border.BorderThicknessProperty, this, BorderThicknessProperty)
            .AltBind(BackgroundProperty, this, DetailBackgroundColorProperty, converter: SolidBrushConverter.Instance);

        _targetedPopup 
                .Padding(0)
                .Opacity(0)
                .Margin(popupMargin)
                .StretchContent()
                .HasShadow(true)
                .PreferredPointerDirection(PointerDirection.Up)
                .FallbackPointerDirection(PointerDirection.Any)
                .PageOverlayBrush(Colors.Transparent)
                .IsPageOverlayHitTestVisible(false)
                .AltBind(TargetedPopup.WeakTargetProperty, this, WeakTargetProperty)
                .AltBind(TargetedPopup.BorderColorProperty, this, DetailBorderColorProperty)
                .AltBind(TargetedPopup.BackgroundColorProperty, this, DetailBackgroundColorProperty)
                .AltBind(TargetedPopup.BorderWidthProperty, this, PopupBorderWidthProperty)
                .AltBind(TargetedPopup.CornerRadiusProperty, this, DetailCornerRadiusProperty)
                .AltBind(MinHeightProperty, this, PopupMinHeightProperty)
                .AltBind(MinWidthProperty, this, PopupMinWidthProperty)
                .AltBind(TargetedPopup.PageOverlayBrushProperty, this, PageOverlayBrushProperty)
                .AltBind(TargetedPopup.IsPageOverlayHitTestVisibleProperty, this, IsPageOverlayHitTestVisibleProperty)
                .AltBind(TargetedPopup.PopOnPageOverlayTouchProperty, this, PopOnPageOverlayTouchProperty)
                .AltBind(TargetedPopup.HorizontalAlignmentProperty, this, PopupHorizontalAlignmentProperty)
                .AltBind(TargetedPopup.VerticalAlignmentProperty, this, PopupVerticalAlignmentProperty)
                .AddPoppedHandler(OnTargetedPopupPopped)
            ;

        PageOverlayBrush = Colors.Black.WithAlpha(0.01).ToBrush();
            

    }


}
