﻿using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using P42.Uno.Markup;
using P42.Utils.Uno;
using Microsoft.UI.Xaml.Input;
using System;
using Microsoft.UI;

namespace P42.Uno.Controls
{
    public partial class TargetedPopup : ContentControl
    {
        #region Defaults
        const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;
        const VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Center;
        const double DefaultBorderThickness = 1;
        const double DefaultCornerRadius = 4;
        #endregion

        #region Visual Elements
        internal SkiaBubble ShadowBorder;
        internal BubbleBorder ContentBorder;
        internal Rectangle PageOverlay;
        #endregion

        void Build()
        {
            /*
            ContentPresenter = new ContentPresenter()
                .Bind(ContentPresenter.ContentProperty, this, nameof(Content))
                .Bind(ContentPresenter.ContentTemplateProperty, this, nameof(ContentTemplate))
                .Bind(ContentPresenter.ContentTemplateSelectorProperty, this, nameof(ContentTemplateSelector))
                .Bind(ContentPresenter.ContentTransitionsProperty, this, nameof(ContentTransitions))
                .Bind(ContentPresenter.CharacterSpacingProperty, this, nameof(CharacterSpacing))
                .BindFont(this)
                .Bind(ContentPresenter.HorizontalContentAlignmentProperty, this, nameof(HorizontalContentAlignment))
                .Bind(ContentPresenter.IsTextScaleFactorEnabledProperty, this, nameof(IsTextScaleFactorEnabled))
                //.Bind(ContentPresenter.LineHeightProperty, this, nameof(LineHeight))
                //.Bind(ContentPresenter.LineStackingStrategy, this, nameof(LineStackingStrategy))
                //.Bind(ContentPresenter.MaxLinesProperty, this, nameof(MaxLines))
                //.Bind(ContentPresenter.OpticalMarginAlignmentProperty, this, nameof(OpticalMarginAlignment))
                .Bind(ContentPresenter.PaddingProperty, this, nameof(Padding))
                //.Bind(TextLineBoundsProperty, this, nameof(TextLineBounds))
                //.Bind(ContentPresenter.TextWrappingProperty, this, nameof(TextWrapping))
                .Bind(ContentPresenter.VerticalContentAlignmentProperty, this, nameof(VerticalContentAlignment));
            */
            
            PageOverlay = new Rectangle()
                .Stretch()
                .Bind(Rectangle.FillProperty, this, nameof(PageOverlayBrush))
                .Bind(Rectangle.IsHitTestVisibleProperty, this, nameof(IsPageOverlayHitTestVisible))
                .Bind(Rectangle.VisibilityProperty, this, nameof(PageOverlayBrush), converter: P42.Utils.Uno.VisibilityExtensions.VisibilityConverter)
                .AddTappedHandler(OnPageOverlayTapped);
            
            //TODO: implement binding of properties to BubbleBorder.ContentPresenter 
            ContentBorder = new BubbleBorder()
                .HitTestVisible(true)
                .Bind(BubbleBorder.ContentProperty, this, nameof(Content))
                //.Bind(BubbleBorder.ContentTemplateProperty, this, nameof(ContentTemplate))
                //.Bind(BubbleBorder.ContentTemplateSelectorProperty, this, nameof(ContentTemplateSelector))
                //.Bind(BubbleBorder.ContentTransitionsProperty, this, nameof(ContentTransitions))
                //.BindFont(this)
                //.Bind(BubbleBorder.HorizontalContentAlignmentProperty, this, nameof(HorizontalContentAlignment))
                //.Bind(BubbleBorder.VerticalContentAlignmentProperty, this, nameof(VerticalContentAlignment))
                .Bind(BubbleBorder.PaddingProperty, this, nameof(Padding))
                .Bind(BubbleBorder.BackgroundProperty, this, nameof(Background))
                .Bind(BubbleBorder.BorderBrushProperty, this, nameof(BorderBrush))
                .Bind(BubbleBorder.BorderWidthProperty, this, nameof(BorderWidth))
                .Bind(BubbleBorder.CornerRadiusProperty, this, nameof(CornerRadius))
                .Bind(BubbleBorder.PointerCornerRadiusProperty, this, nameof(PointerCornerRadius))
                .Bind(BubbleBorder.PointerLengthProperty, this, nameof(PointerLength))
                .Bind(BubbleBorder.PointerTipRadiusProperty, this, nameof(PointerTipRadius))
                .AddSizeChangedHandler(OnBorderSizeChanged);

            ShadowBorder = new SkiaBubble()
                .Bind(SkiaBubble.VisibilityProperty, this, nameof(HasShadow), converter: P42.Utils.Uno.VisibilityExtensions.VisibilityConverter)
                .Bind(SkiaBubble.PointerCornerRadiusProperty, ContentBorder, nameof(BubbleBorder.PointerCornerRadius))
                .Bind(SkiaBubble.PointerDirectionProperty, ContentBorder, nameof(BubbleBorder.PointerDirection))
                .Bind(SkiaBubble.PointerLengthProperty, ContentBorder, nameof(BubbleBorder.PointerLength))
                .Bind(SkiaBubble.PointerTipRadiusProperty, ContentBorder, nameof(BubbleBorder.PointerTipRadius))
                .IsShadow();


            RootFrame.Current.SizeChanged += OnRootFrameSizeChanged;

            ActualPointerDirection = PointerDirection.None;
            Background = SystemTeachingTipBrushes.Background;
            BorderBrush = SystemTeachingTipBrushes.Border;
            Foreground = SystemTeachingTipBrushes.Foreground;
            PageOverlayBrush = new SolidColorBrush(Colors.Black.WithAlpha(0.25));
            HorizontalContentAlignment = HorizontalAlignment.Left;
            VerticalContentAlignment = VerticalAlignment.Top;
            MinWidth = 40;
            MinHeight = 40;


        }

        protected virtual void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
            => UpdateMarginAndAlignment();

        protected virtual void OnBorderSizeChanged(object sender, SizeChangedEventArgs args)
        {
            if (HasShadow)
            {
                var ΔHeight = args.NewSize.Height - (ShadowBorder.Height + ShadowBorder.BlurSigma * 4);
                var ΔWidth = args.NewSize.Width - (ShadowBorder.Width + ShadowBorder.BlurSigma * 4);
                if (ΔWidth <= 0 && ΔWidth > -2 && ΔHeight <= 0 && ΔHeight > -2)
                    return;

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


}
