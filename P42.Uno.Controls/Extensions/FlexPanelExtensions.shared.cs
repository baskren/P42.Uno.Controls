using System;
using Windows.UI.Xaml;
using EType = Windows.UI.Xaml.UIElement;

namespace P42.Uno.Controls
{
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

        public static TElement AlignContent<TElement>(this TElement panel, FlexAlignContent value) where TElement : FlexPanel
        { panel.AlignContent = value; return panel; }

        public static TElement AlignItems<TElement>(this TElement panel, FlexAlignItems value) where TElement : FlexPanel
        { panel.AlignItems = value; return panel; }

        public static TElement Direction<TElement>(this TElement panel, FlexDirection value) where TElement : FlexPanel
        { panel.Direction = value; return panel; }

        public static TElement JustifyContent<TElement>(this TElement panel, FlexJustify value) where TElement : FlexPanel
        { panel.JustifyContent = value; return panel; }

        public static TElement Wrap<TElement>(this TElement panel, FlexWrap value) where TElement : FlexPanel
        { panel.Wrap = value; return panel; }


    }
}