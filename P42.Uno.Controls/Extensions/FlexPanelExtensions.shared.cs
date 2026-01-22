using EType = Microsoft.UI.Xaml.UIElement;

namespace P42.Uno.Controls;

public static class FlexPanelExtensions
{
    public static TElement FlexPanelOrder<TElement>(this TElement element, int order) where TElement : EType
    { FlexPanel.SetOrder(element, order); return element; }

    public static TElement FlexPanelGrow<TElement>(this TElement element, double value) where TElement : EType
    { FlexPanel.SetGrow(element, value); return element; }

    public static TElement FlexPanelShrink<TElement>(this TElement element, double value) where TElement : EType
    { FlexPanel.SetShrink(element, value); return element; }

    public static TElement FlexPanelAlignSelf<TElement>(this TElement element, FlexAlignSelf value) where TElement : EType
    { FlexPanel.SetAlignSelf(element, value); return element; }

    public static TElement FlexPanelBasis<TElement>(this TElement element, FlexBasis value) where TElement : EType
    { FlexPanel.SetFlexBasis(element, value); return element; }


}
