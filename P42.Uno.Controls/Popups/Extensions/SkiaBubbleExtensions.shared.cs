using Windows.UI;
using ElementType = P42.Uno.Controls.SkiaBubble;

namespace P42.Uno.Controls;

public static class SkiaBubbleExtensions
{


    #region Pointer Direction
    public static TElement DownPointerDirection<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Down; return element; }

    public static TElement UpPointerDirection<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Up; return element; }

    public static TElement LeftPointerDirection<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Left; return element; }

    public static TElement RightPointerDirection<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Right; return element; }

    public static TElement HorizontalPointerDirection<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Horizontal; return element; }

    public static TElement VerticalPointerDirection<TElement>(this TElement element) where TElement : ElementType
    { element.PointerDirection = Controls.PointerDirection.Vertical; return element; }

    public static TElement AnyPointerDirection<TElement>(this TElement element) where TElement : ElementType
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


}


