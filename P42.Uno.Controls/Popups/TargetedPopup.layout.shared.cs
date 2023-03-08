using Windows.UI;
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
        internal NewBubbleBorder ContentBorder;
        internal Rectangle PageOverlay;
        ContentPresenter ContentPresenter;
        #endregion

        void Build()
        {

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

            PageOverlay = new Rectangle()
                .Stretch()
                .Bind(Rectangle.FillProperty, this, nameof(PageOverlayBrush))
                .Bind(Rectangle.IsHitTestVisibleProperty, this, nameof(IsPageOverlayHitTestVisible))
                .Bind(Rectangle.VisibilityProperty, this, nameof(PageOverlayBrush), converter: P42.Utils.Uno.VisibilityExtensions.VisibilityConverter)
                .AddTappedHandler(OnPageOverlayTapped);

            //TODO: return to BubbleBorder and implement binding of properties to BubbleBorder.ContentPresenter 
            ContentBorder = new NewBubbleBorder()
                .HitTestVisible(true)
                .Content(ContentPresenter)
                .Bind(NewBubbleBorder.PaddingProperty, this, nameof(Padding))
                .Bind(NewBubbleBorder.BackgroundColorProperty, this, nameof(BackgroundColor))
                .Bind(NewBubbleBorder.BorderColorProperty, this, nameof(BorderColor))
                .Bind(NewBubbleBorder.BorderWidthProperty, this, nameof(BorderWidth))
                .Bind(NewBubbleBorder.CornerRadiusProperty, this, nameof(CornerRadius))
                .Bind(NewBubbleBorder.PointerCornerRadiusProperty, this, nameof(PointerCornerRadius))
                .Bind(NewBubbleBorder.PointerLengthProperty, this, nameof(PointerLength))
                .Bind(NewBubbleBorder.PointerTipRadiusProperty, this, nameof(PointerTipRadius))
                .AddSizeChangedHandler(OnBorderSizeChanged);

            ShadowBorder = new SkiaBubble()
                .Bind(SkiaBubble.VisibilityProperty, this, nameof(HasShadow), converter: P42.Utils.Uno.VisibilityExtensions.VisibilityConverter)
                .Bind(SkiaBubble.PointerCornerRadiusProperty, ContentBorder, nameof(NewBubbleBorder.PointerCornerRadius))
                .Bind(SkiaBubble.PointerDirectionProperty, ContentBorder, nameof(NewBubbleBorder.PointerDirection))
                .Bind(SkiaBubble.PointerLengthProperty, ContentBorder, nameof(NewBubbleBorder.PointerLength))
                .Bind(SkiaBubble.PointerTipRadiusProperty, ContentBorder, nameof(NewBubbleBorder.PointerTipRadius))
                .IsShadow();


            RootFrame.Current.SizeChanged += OnRootFrameSizeChanged;

            ActualPointerDirection = PointerDirection.None;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            PageOverlayBrush = new SolidColorBrush(Colors.Black.WithAlpha(0.25));
            VerticalContentAlignment = VerticalAlignment.Stretch;
            MinWidth = 40;
            MinHeight = 40;


        }

        protected virtual void OnRootFrameSizeChanged(object sender, SizeChangedEventArgs e)
            => UpdateMarginAndAlignment();

        protected virtual void OnBorderSizeChanged(object sender, SizeChangedEventArgs args)
        {
            ShadowBorder.Height = ContentBorder.ActualHeight + ShadowBorder.BlurSigma * 4;
            ShadowBorder.Width = ContentBorder.ActualWidth + ShadowBorder.BlurSigma * 4;
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
