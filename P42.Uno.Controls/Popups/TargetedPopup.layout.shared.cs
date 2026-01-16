namespace P42.Uno.Controls;

[Bindable]
public partial class TargetedPopup : ContentControl
{
    #region Defaults

    private const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;
    private const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Center;
    private const double DefaultBorderThickness = 1;
    private const double DefaultCornerRadius = 4;
    #endregion

    #region Visual Elements
    internal readonly SkiaBubble ShadowBorder = new();
    internal readonly BubbleBorder ContentBorder = new();
    internal readonly Rectangle PageOverlay = new();
    #endregion

    private void Build()
    {
        this.Foreground(SystemColors.BaseHigh);
        
        PageOverlay
            .Stretch()
            .AltBind(Shape.FillProperty, this, PageOverlayBrushProperty)
            .AltBind(IsHitTestVisibleProperty, this, IsPageOverlayHitTestVisibleProperty)
            .BindVisible( this, PageOverlayVisibleProperty)
            .AddTappedHandler(OnPageOverlayTapped);
        
        ContentBorder
            .IsHitTestVisible(true)
            .BindVisible(this, VisibilityProperty)
            .AltBind(BubbleBorder.ContentProperty, this, ContentProperty)
            .AltBind(BubbleBorder.ContentTemplateProperty, this, ContentTemplateProperty)
            .AltBind(BubbleBorder.ContentTemplateSelectorProperty, this, ContentTemplateSelectorProperty)
            .AltBind(BubbleBorder.ContentTransitionsProperty, this, ContentTransitionsProperty)
            .AltBind(BubbleBorder.HorizontalContentAlignmentProperty, this, HorizontalContentAlignmentProperty)
            .AltBind(BubbleBorder.VerticalContentAlignmentProperty, this, VerticalContentAlignmentProperty)
            .AltBind(BubbleBorder.PaddingProperty, this, PaddingProperty)
            .AltBind(BubbleBorder.BackgroundColorProperty, this, BackgroundColorProperty)
            .AltBind(BubbleBorder.BorderColorProperty, this, BorderColorProperty)
            .AltBind(BubbleBorder.BorderWidthProperty, this, BorderWidthProperty)
            .AltBind(BubbleBorder.CornerRadiusProperty, this, CornerRadiusProperty)
            .AltBind(BubbleBorder.PointerCornerRadiusProperty, this, PointerCornerRadiusProperty)
            .AltBind(BubbleBorder.PointerLengthProperty, this, PointerLengthProperty)
            .AltBind(BubbleBorder.PointerTipRadiusProperty, this, PointerTipRadiusProperty)
            .BindFont(this)
            .AddSizeChangedHandler(OnBorderSizeChanged);

        ShadowBorder
            .BindVisible(this, ShadowVisibleProperty)
            .AltBind(SkiaBubble.CornerRadiusProperty, this, CornerRadiusProperty)
            .AltBind(SkiaBubble.PointerCornerRadiusProperty, ContentBorder, BubbleBorder.PointerCornerRadiusProperty)
            .AltBind(SkiaBubble.PointerDirectionProperty, ContentBorder, BubbleBorder.PointerDirectionProperty)
            .AltBind(SkiaBubble.PointerLengthProperty, ContentBorder, BubbleBorder.PointerLengthProperty)
            .AltBind(SkiaBubble.PointerTipRadiusProperty, ContentBorder, BubbleBorder.PointerTipRadiusProperty)
            .IsShadow();


        ActualPointerDirection = PointerDirection.None;
        //Background = SystemTeachingTipBrushes.Background;
        //BorderBrush = SystemTeachingTipBrushes.Border;
        Foreground = SystemTeachingTipBrushes.Foreground;
        HorizontalContentAlignment = HorizontalAlignment.Left;
        VerticalContentAlignment = VerticalAlignment.Top;
        MinWidth = 40;
        MinHeight = 40;


    }

    protected virtual void OnPopupFrameSizeChanged(object sender, SizeChangedEventArgs args)
    {
        //System.Diagnostics.Debug.WriteLine($"TargetedPopup.OnPopupFrameSizeChanged : {args.PreviousSize} => {args.NewSize}");
        UpdateMarginAndAlignment();
    }

    protected virtual void OnBorderSizeChanged(object sender, SizeChangedEventArgs args)
    {
        //System.Diagnostics.Debug.WriteLine($"TargetedPopup.OnBorderSizechanged : {args.PreviousSize} => {args.NewSize}");

        if (Math.Abs(args.PreviousSize.Width - args.NewSize.Width) < 1 && Math.Abs(args.PreviousSize.Height - args.NewSize.Height) < 1)
            return;

        //if (args.PreviousSize != default)
        //    UpdateMarginAndAlignment(args.NewSize);

        if (HasShadow)
        {
            /*
            var ΔHeight = args.NewSize.Height - (ShadowBorder.Height + ShadowBorder.BlurSigma * 4);
            var ΔWidth = args.NewSize.Width - (ShadowBorder.Width + ShadowBorder.BlurSigma * 4);
            if (ΔWidth <= 0 && ΔWidth > -2 && ΔHeight <= 0 && ΔHeight > -2)
                return;
            */
            ShadowBorder.Height = ContentBorder.ActualHeight + ShadowBorder.BlurSigma * 4;
            ShadowBorder.Width = ContentBorder.ActualWidth + ShadowBorder.BlurSigma * 4;
        }
    }

    private async void OnPageOverlayTapped(object sender, TappedRoutedEventArgs e)
    {
        if (PopOnPageOverlayTouch)
        {
            await PopAsync(PopupPoppedCause.BackgroundTouch);
            e.Handled = true;
        }
    }


}
