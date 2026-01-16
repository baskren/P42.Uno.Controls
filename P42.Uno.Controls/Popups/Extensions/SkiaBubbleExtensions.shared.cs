using Windows.UI;
using ElementType = P42.Uno.Controls.SkiaBubble;

namespace P42.Uno.Controls;

public static class SkiaBubbleExtensions
{

    #region Background Color
    
    public static TElement BackgroundColor<TElement>(this TElement element, uint hex) where TElement : ElementType
    { element.BackgroundColor = ColorExtensions.ColorFromUint(hex); return element; }
    #endregion

    #region BorderColor Color

    public static TElement BorderColor<TElement>(this TElement element, uint hex) where TElement : ElementType
    { element.BorderColor = ColorExtensions.ColorFromUint(hex); return element; }
    #endregion

    #region Pointer Direction
    public static TElement PointerDown<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Down; return element; }

    public static TElement PointerUp<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Up; return element; }

    public static TElement PointerLeft<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Left; return element; }

    public static TElement PointerRight<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Right; return element; }

    public static TElement PointerHorizontal<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Horizontal; return element; }

    public static TElement PointerVertical<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Vertical; return element; }

    public static TElement PointerAny<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Any; return element; }
    #endregion

    internal static TElement IsShadow<TElement>(this TElement element) where TElement : ElementType
    {
        element
            .BackgroundColor(Colors.Black.WithAlpha(0.5))
            .IsHitTestVisible(false)
            .Translate(element.BlurSigma, element.BlurSigma)
            .BorderWidth(0);
        element.ApplyBlur = true;
        return element;
    }

    /*
    public static TElement Size<TElement>(this TElement element, Size value) where TElement : ElementType
    { element.Width = value.Width; element.Height = value.Height; return element; }

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
    */

}


