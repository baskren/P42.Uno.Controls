using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Animation;
using P42.Utils.Uno;
using Windows.UI;
using Windows.UI.Text;
using ElementType = P42.Uno.Controls.BubbleBorder;

namespace P42.Uno.Controls;

public static class BubbleBorderExtensions
{


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


}

