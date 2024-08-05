using Windows.UI;
using P42.Uno.Markup;
using Microsoft.UI;
using ElementType = P42.Uno.Controls.SkiaBubble;

namespace P42.Uno.Controls
{
    public static class SkiaBubbleExtensions
    {

        #region Background Color
        public static TElement BackgroundColor<TElement>(this TElement element, Color color) where TElement : ElementType
        { element.BackgroundColor = color; return element; }

        public static TElement BackgroundColor<TElement>(this TElement element, string color) where TElement : ElementType
        { element.BackgroundColor = ColorExtensions.ColorFromString(color); return element; }

        public static TElement BackgroundColor<TElement>(this TElement element, uint hex) where TElement : ElementType
        { element.BackgroundColor = ColorExtensions.ColorFromUint(hex); return element; }
        #endregion

        #region BorderColor Color
        public static TElement BorderColor<TElement>(this TElement element, Color color) where TElement : ElementType
        { element.BorderColor = color; return element; }

        public static TElement BorderColor<TElement>(this TElement element, string color) where TElement : ElementType
        { element.BorderColor = ColorExtensions.ColorFromString(color); return element; }

        public static TElement BorderColor<TElement>(this TElement element, uint hex) where TElement : ElementType
        { element.BorderColor = ColorExtensions.ColorFromUint(hex); return element; }
        #endregion

        public static TElement BorderWidth<TElement>(this TElement element, double value) where TElement : ElementType
        { element.BorderWidth = value; return element; }

        public static TElement CornerRadius<TElement>(this TElement element, double value) where TElement : ElementType
        { element.CornerRadius = value; return element; }

        public static TElement PointerLength<TElement>(this TElement element, double value) where TElement : ElementType
        { element.PointerLength = value; return element; }

        public static TElement PointerTipRadius<TElement>(this TElement element, double value) where TElement : ElementType
        { element.PointerTipRadius = value; return element; }

        public static TElement PointerAxialPosition<TElement>(this TElement element, double value) where TElement : ElementType
        { element.PointerAxialPosition = value; return element; }

        public static TElement PointerDirection<TElement>(this TElement element, PointerDirection value) where TElement : ElementType
        { element.PointerDirection = value; return element; }

        #region Pointer Direction
        public static TElement PointerDown<TElement>(this TElement element) where TElement : ElementType
        { element.PointerDirection = P42.Uno.Controls.PointerDirection.Down; return element; }

        public static TElement PointerUp<TElement>(this TElement element) where TElement : ElementType
        { element.PointerDirection = P42.Uno.Controls.PointerDirection.Up; return element; }

        public static TElement PointerLeft<TElement>(this TElement element) where TElement : ElementType
        { element.PointerDirection = P42.Uno.Controls.PointerDirection.Left; return element; }

        public static TElement PointerRight<TElement>(this TElement element) where TElement : ElementType
        { element.PointerDirection = P42.Uno.Controls.PointerDirection.Right; return element; }

        public static TElement PointerHorizontal<TElement>(this TElement element) where TElement : ElementType
        { element.PointerDirection = P42.Uno.Controls.PointerDirection.Horizontal; return element; }

        public static TElement PointerVertical<TElement>(this TElement element) where TElement : ElementType
        { element.PointerDirection = P42.Uno.Controls.PointerDirection.Vertical; return element; }

        public static TElement PointerAny<TElement>(this TElement element) where TElement : ElementType
        { element.PointerDirection = P42.Uno.Controls.PointerDirection.Any; return element; }
        #endregion

        public static TElement PointerCornerRadius<TElement>(this TElement element, double value) where TElement : ElementType
        { element.PointerCornerRadius = value; return element; }

        internal static TElement IsShadow<TElement>(this TElement element) where TElement : ElementType
        {
            element
                .BackgroundColor(Microsoft.UI.Colors.Black.WithAlpha(0.5))
                .HitTestVisible(false)
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
}
