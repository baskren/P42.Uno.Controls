using Windows.Foundation;
using Windows.UI;
using ElementType = P42.Uno.Controls.TargetedPopup;

namespace P42.Uno.Controls;

public static class TargetedPopupExtensions

{
    // Redundant with P42.Uno.Markup.ContentControl?
    //public static TElement Content<TElement>(this TElement element, object value) where TElement : ElementType
    //{ element.Content = value; return element; }

    #region Override Properties

    #region Alignment

    public static TElement Center<TElement>(this TElement element) where TElement : ElementType
    {
        element.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center;
        element.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center;
        return element;
    }

    public static TElement Stretch<TElement>(this TElement element) where TElement : ElementType
    {
        element.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Stretch;
        element.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
        return element;
    }

    #region Vertical Alignment
    public static TElement VerticalAlignment<TElement>(this TElement element, VerticalAlignment verticalAlignment) where TElement : ElementType
    { element.VerticalAlignment = verticalAlignment; return element; }

    public static TElement Top<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Top; return element; }

    public static TElement CenterVertical<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center; return element; }

    public static TElement Bottom<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Bottom; return element; }

    public static TElement StretchVertical<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Stretch; return element; }

    #endregion

    #region Horizontal Alignment
    public static TElement HorizontalAlignment<TElement>(this TElement element, HorizontalAlignment horizontalAlignment) where TElement : ElementType
    { element.HorizontalAlignment = horizontalAlignment; return element; }

    public static TElement Left<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Left; return element; }

    public static TElement CenterHorizontal<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center; return element; }

    public static TElement Right<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Right; return element; }

    public static TElement StretchHorizontal<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch; return element; }
    #endregion

    #endregion

    #region Margin

    public static TElement Margin<TElement>(this TElement element, double value) where TElement : ElementType
    { element.Margin = new Thickness(value); return element; }

    public static TElement Margin<TElement>(this TElement element, double horizontal, double vertical) where TElement : ElementType
    { element.Margin = new Thickness(horizontal, vertical, horizontal, vertical); return element; }

    public static TElement Margin<TElement>(this TElement element, double left, double top, double right, double bottom) where TElement : ElementType
    { element.Margin = new Thickness(left, top, right, bottom); return element; }

    public static TElement Margin<TElement>(this TElement element, Thickness margin) where TElement : ElementType
    { element.Margin = margin; return element; }

    #endregion

    public static TElement BorderWidth<TElement>(this TElement element, double value) where TElement : ElementType
    { element.BorderWidth = value; return element; }

    // redundant with P42.Uno.Markup.ControlExtensions
    public static TElement CornerRadius<TElement>(this TElement element, double value) where TElement : ElementType
    { element.CornerRadius = value; return element; }

    #endregion

    #region Target Properties
    public static TElement Target<TElement>(this TElement element, UIElement value) where TElement : ElementType
    { element.Target = value; return element; }

    public static TElement Target<TElement>(this TElement element, Rect value) where TElement : ElementType
    { element.TargetRect = value; return element; }
    #endregion

    #region Pointer Properties
    public static TElement PointerBias<TElement>(this TElement element, double value) where TElement : ElementType
    { element.PointerBias = value; return element; }

    public static TElement PointerCornerRadius<TElement>(this TElement element, double value) where TElement : ElementType
    { element.PointerCornerRadius = value; return element; }

    #region Pointer Direction
    public static TElement PreferredPointerDirection<TElement>(this TElement element, PointerDirection value) where TElement : ElementType
    { element.PreferredPointerDirection = value; return element; }

    public static TElement FallbackPointerDirection<TElement>(this TElement element, PointerDirection value) where TElement : ElementType
    { element.FallbackPointerDirection = value; return element; }

    public static TElement PreferredPointerDown<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Down; return element; }

    public static TElement PreferredPointerUp<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Up; return element; }

    public static TElement PreferredPointerLeft<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Left; return element; }

    public static TElement PreferredPointerRight<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Right; return element; }

    public static TElement PreferredPointerHorizontal<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Horizontal; return element; }

    public static TElement PreferredPointerVertical<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Vertical; return element; }

    public static TElement PreferredPointerAny<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Any; return element; }

    public static TElement FallbackPointerDown<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Down; return element; }

    public static TElement FallbackPointerUp<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Up; return element; }

    public static TElement FallbackPointerLeft<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Left; return element; }

    public static TElement FallbackPointerRight<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Right; return element; }

    public static TElement FallbackPointerHorizontal<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Horizontal; return element; }

    public static TElement FallbackPointerVertical<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Vertical; return element; }

    public static TElement FallbackPointerAny<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Any; return element; }
    #endregion

    public static TElement PointerLength<TElement>(this TElement element, double value) where TElement : ElementType
    { element.PointerLength = value; return element; }

    public static TElement PointerTipRadius<TElement>(this TElement element, double value) where TElement : ElementType
    { element.PointerTipRadius = value; return element; }

    public static TElement PointOffScreen<TElement>(this TElement element, bool value = true) where TElement : ElementType
    { element.PointToOffScreenElements = value; return element; }

    public static TElement PointerMargin<TElement>(this TElement element, double value) where TElement : ElementType
    { element.PointerMargin = value; return element; }


    #endregion

    #region PageOverlay Properties

    #region PageOverlayBrush
    public static TElement PageOverlay<TElement>(this TElement element, Brush brush) where TElement : ElementType
    { element.PageOverlayBrush = brush; return element; }

    public static TElement PageOverlay<TElement>(this TElement element, Color color) where TElement : ElementType
    { element.PageOverlayBrush = new SolidColorBrush(color); return element; }

    public static TElement PageOverlay<TElement>(this TElement element, string color) where TElement : ElementType
    { element.PageOverlayBrush = new SolidColorBrush(color.ColorFromString()); return element; }

    public static TElement PageOverlay<TElement>(this TElement element, uint hex) where TElement : ElementType
    { element.PageOverlayBrush = new SolidColorBrush(ColorExtensions.ColorFromUint(hex)); return element; }
    #endregion

    public static TElement PageOverlayHitTestVisible<TElement>(this TElement element, bool value = true) where TElement : ElementType
    { element.IsPageOverlayHitTestVisible = value; return element; }

    #endregion

    public static TElement HasShadow<TElement>(this TElement element, bool value = true) where TElement : ElementType
    { element.HasShadow = value; return element; }

    #region Push/Pop Properties
    public static TElement PopAfter<TElement>(this TElement element, TimeSpan value) where TElement : ElementType
    { element.PopAfter = value; return element; }

    public static TElement PopOnPointerMove<TElement>(this TElement element, bool value = true) where TElement : ElementType
    { element.PopOnPointerMove = value; return element; }

    public static TElement PopOnPageOverlayTouch<TElement>(this TElement element, bool value = true) where TElement : ElementType
    { element.PopOnPageOverlayTouch = value; return element; }

    public static TElement PopOnBackButtonClick<TElement>(this TElement element, bool value = true) where TElement : ElementType
    { element.PopOnBackButtonClick = value; return element; }

    public static TElement Parameter<TElement>(this TElement element, object value) where TElement : ElementType
    { element.Parameter = value; return element; }

    public static TElement PushEffect<TElement>(this TElement element, Effect value, EffectMode? mode = null) where TElement : ElementType
    { 
        element.PushEffect = value;
        if (mode is { } effectMode)
            element.PushEffectMode = effectMode;
        return element; 
    }

    public static TElement PushEffectMode<TElement>(this TElement element, EffectMode value) where TElement : ElementType
    { element.PushEffectMode = value; return element; }

    public static TElement AnimationDuration<TElement>(this TElement element, TimeSpan value) where TElement : ElementType
    { element.AnimationDuration = value; return element; }

    #endregion


    #region Events
    public static TElement AddPushedHandler<TElement>(this TElement element, EventHandler handler) where TElement : ElementType
    { element.Pushed += handler; return element; }

    public static TElement AddPoppedHandler<TElement>(this TElement element, EventHandler<PopupPoppedEventArgs> handler) where TElement : ElementType
    { element.Popped += handler; return element; }

    #endregion
}
