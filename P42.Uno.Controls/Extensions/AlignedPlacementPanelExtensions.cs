using ElementType = P42.Uno.Controls.AlignedPlacementPanel;

namespace P42.Uno.Controls;

public static class AlignedPlacementPanelExtensions
{
    public static TElement Spacing<TElement>(this TElement element, double value) where TElement : ElementType
    { element.Spacing = value; return element; }

    #region Padding
    public static TElement Padding<TElement>(this TElement element, double value) where TElement : ElementType
    { element.Padding = new Thickness(value); return element; }

    public static TElement Padding<TElement>(this TElement element, double horizontal, double vertical) where TElement : ElementType
    { element.Padding = new Thickness(horizontal, vertical, horizontal, vertical); return element; }

    public static TElement Padding<TElement>(this TElement element, double left, double top, double right, double bottom) where TElement : ElementType
    { element.Padding = new Thickness(left, top, right, bottom); return element; }

    public static TElement Padding<TElement>(this TElement element, Thickness padding) where TElement : ElementType
    { element.Padding = padding; return element; }
    #endregion

}
