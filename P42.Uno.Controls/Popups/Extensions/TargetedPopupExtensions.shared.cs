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


    #endregion


    #region Pointer Properties


    #region Pointer Direction

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



    #endregion

    #region PageOverlay Properties

    #region PageOverlayBrush
    public static TElement PageOverlay<TElement>(this TElement element, Color color) where TElement : ElementType
    { element.PageOverlayBrush = new SolidColorBrush(color); return element; }

    public static TElement PageOverlay<TElement>(this TElement element, uint hex) where TElement : ElementType
    { element.PageOverlayBrush = new SolidColorBrush(ColorExtensions.ColorFromUint(hex)); return element; }
    #endregion

    #endregion


    #region Push/Pop Properties

    public static TElement PushEffect<TElement>(this TElement element, Effect value, EffectMode mode) where TElement : ElementType
    { 
        element.PushEffect = value;
        element.PushEffectMode = mode;
        return element; 
    }

    #endregion



    #region Content Alignment

    public static TElement ContentCenter<TElement>(this TElement element) where TElement : ElementType
    {
        element.HorizontalContentAlignment = HorizontalAlignment.Center;
        element.VerticalContentAlignment = VerticalAlignment.Center;
        return element;
    }

    public static TElement ContentStretch<TElement>(this TElement element) where TElement : ElementType
    {
        element.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        element.VerticalContentAlignment = VerticalAlignment.Stretch;
        return element;
    }

    #region Horizontal
    public static TElement HorizontalContentAlignment<TElement>(this TElement element, HorizontalAlignment value) where TElement : ElementType
    { element.HorizontalContentAlignment = value; return element; }

    public static TElement ContentLeft<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalContentAlignment = HorizontalAlignment.Left; return element; }

    public static TElement ContentRight<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalContentAlignment = HorizontalAlignment.Right; return element; }

    public static TElement ContentHorizontalCenter<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalContentAlignment = HorizontalAlignment.Center; return element; }

    public static TElement ContentHorizontalStretch<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalContentAlignment = HorizontalAlignment.Stretch; return element; }
    #endregion

    #region Vertical
    public static TElement VerticalContentAlignment<TElement>(this TElement element, VerticalAlignment value) where TElement : ElementType
    { element.VerticalContentAlignment = value; return element; }

    public static TElement ContentTop<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalContentAlignment = VerticalAlignment.Top; return element; }

    public static TElement ContentBottom<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalContentAlignment = VerticalAlignment.Bottom; return element; }

    public static TElement ContentVerticalCenter<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalContentAlignment = VerticalAlignment.Center; return element; }

    public static TElement ContentVerticalStretch<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalContentAlignment = VerticalAlignment.Stretch; return element; }

    #endregion

    #endregion



    #region Events
    public static TElement AddPushedHandler<TElement>(this TElement element, EventHandler handler) where TElement : ElementType
    { element.Pushed += handler; return element; }

    public static TElement AddPoppedHandler<TElement>(this TElement element, EventHandler<PopupPoppedEventArgs> handler) where TElement : ElementType
    { element.Popped += handler; return element; }

    #endregion


}


