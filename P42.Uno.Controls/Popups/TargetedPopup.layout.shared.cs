using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using Microsoft.UI;
using Windows.Foundation;

namespace P42.Uno.Controls;

[Microsoft.UI.Xaml.Data.Bindable]
public partial class TargetedPopup : ContentControl
{
    #region Defaults
    const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;
    const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Center;
    const double DefaultBorderThickness = 1;
    const double DefaultCornerRadius = 4;
    #endregion

    #region Visual Elements
    internal readonly SkiaBubble ShadowBorder = new();
    internal readonly BubbleBorder ContentBorder = new();
    internal readonly Rectangle PageOverlay = new();
    #endregion

    void Build()
    {

        PageOverlay
            .Stretch()
            .WBind(Rectangle.FillProperty, this, PageOverlayBrushProperty)
            .WBind(Rectangle.IsHitTestVisibleProperty, this, IsPageOverlayHitTestVisibleProperty)
            .WBindVisible( this, PageOverlayVisibleProperty)
            .AddTappedHandler(OnPageOverlayTapped);
        
        ContentBorder
            .HitTestVisible(true)
                .WBindVisible(this, VisibilityProperty)
                .WBind(BubbleBorder.ContentProperty, this, ContentProperty)
                .WBind(BubbleBorder.ContentTemplateProperty, this, ContentTemplateProperty)
                .WBind(BubbleBorder.ContentTemplateSelectorProperty, this, ContentTemplateSelectorProperty)
                .WBind(BubbleBorder.ContentTransitionsProperty, this, ContentTransitionsProperty)
                .WBindFont(this)
                .WBind(BubbleBorder.HorizontalContentAlignmentProperty, this, HorizontalContentAlignmentProperty)
                .WBind(BubbleBorder.VerticalContentAlignmentProperty, this, VerticalContentAlignmentProperty)
                .WBind(BubbleBorder.PaddingProperty, this, PaddingProperty)
                .WBind(BubbleBorder.BackgroundColorProperty, this, BackgroundColorProperty)
                .WBind(BubbleBorder.BorderColorProperty, this, BorderColorProperty)
                .WBind(BubbleBorder.BorderWidthProperty, this, BorderWidthProperty)
                .WBind(BubbleBorder.CornerRadiusProperty, this, CornerRadiusProperty)
                .WBind(BubbleBorder.PointerCornerRadiusProperty, this, PointerCornerRadiusProperty)
                .WBind(BubbleBorder.PointerLengthProperty, this, PointerLengthProperty)
                .WBind(BubbleBorder.PointerTipRadiusProperty, this, PointerTipRadiusProperty)
            .AddSizeChangedHandler(OnBorderSizeChanged);

        ShadowBorder
            .WBindVisible(this, ShadowVisibleProperty)
            .WBind(SkiaBubble.CornerRadiusProperty, this, CornerRadiusProperty)
            .WBind(SkiaBubble.PointerCornerRadiusProperty, ContentBorder, BubbleBorder.PointerCornerRadiusProperty)
            .WBind(SkiaBubble.PointerDirectionProperty, ContentBorder, BubbleBorder.PointerDirectionProperty)
            .WBind(SkiaBubble.PointerLengthProperty, ContentBorder, BubbleBorder.PointerLengthProperty)
            .WBind(SkiaBubble.PointerTipRadiusProperty, ContentBorder, BubbleBorder.PointerTipRadiusProperty)
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

    async void OnPageOverlayTapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        if (PopOnPageOverlayTouch)
        {
            await PopAsync(PopupPoppedCause.BackgroundTouch);
            e.Handled = true;
        }
    }


}
