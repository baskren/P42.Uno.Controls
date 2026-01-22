using Windows.Foundation;
using Windows.UI;
using ElementType = P42.Uno.Controls.TargetedPopup;

namespace P42.Uno.Controls;


public static class TargetedPopupExtensions

{


    #region Pointer Direction

    public static TElement DownPreferredPointer<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Down; return element; }

    public static TElement UpPreferredPointer<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Up; return element; }

    public static TElement LeftPreferredPointer<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Left; return element; }

    public static TElement RightPreferredPointer<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Right; return element; }

    public static TElement HorizontalPreferredPointer<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Horizontal; return element; }

    public static TElement VerticalPreferredPointer<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Vertical; return element; }

    public static TElement AnyPreferredPointer<TElement>(this TElement element) where TElement : ElementType
    { element.PreferredPointerDirection = PointerDirection.Any; return element; }

    public static TElement DownFallbackPointer<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Down; return element; }

    public static TElement UpFallbackPointer<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Up; return element; }

    public static TElement LeftFallbackPointer<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Left; return element; }

    public static TElement RightFallbackPointer<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Right; return element; }

    public static TElement HorizontalFallbackPointer<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Horizontal; return element; }

    public static TElement VerticalFallbackPointer<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Vertical; return element; }

    public static TElement AnyFallbackPointer<TElement>(this TElement element) where TElement : ElementType
    { element.FallbackPointerDirection = PointerDirection.Any; return element; }
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

    public static TElement CenterContentAlignment<TElement>(this TElement element) where TElement : ElementType
    {
        element.HorizontalContentAlignment = HorizontalAlignment.Center;
        element.VerticalContentAlignment = VerticalAlignment.Center;
        return element;
    }

    public static TElement StretchContentAlignment<TElement>(this TElement element) where TElement : ElementType
    {
        element.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        element.VerticalContentAlignment = VerticalAlignment.Stretch;
        return element;
    }

    #region Horizontal
    public static TElement LeftContentAlignment<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalContentAlignment = HorizontalAlignment.Left; return element; }

    public static TElement RightContentAlignment<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalContentAlignment = HorizontalAlignment.Right; return element; }

    public static TElement CenterHzContentAlignment<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalContentAlignment = HorizontalAlignment.Center; return element; }

    public static TElement StretchHzContentAlignment<TElement>(this TElement element) where TElement : ElementType
    { element.HorizontalContentAlignment = HorizontalAlignment.Stretch; return element; }
    #endregion

    #region Vertical
    public static TElement TopContentAlignment<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalContentAlignment = VerticalAlignment.Top; return element; }

    public static TElement BottomContentAlignment<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalContentAlignment = VerticalAlignment.Bottom; return element; }

    public static TElement CenterVtContentAlignment<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalContentAlignment = VerticalAlignment.Center; return element; }

    public static TElement StretchVtContentAlignment<TElement>(this TElement element) where TElement : ElementType
    { element.VerticalContentAlignment = VerticalAlignment.Stretch; return element; }

    #endregion

    #endregion




}


