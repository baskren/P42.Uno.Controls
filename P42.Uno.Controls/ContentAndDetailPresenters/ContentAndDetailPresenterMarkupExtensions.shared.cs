using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using P42.Uno.Markup;
using Windows.UI.ViewManagement;
using ElementType = P42.Uno.Controls.ContentAndDetailPresenter;

namespace P42.Uno.Controls
{
    public static class ContentAndDetailPresenterMarkupExtensions
    {
        public static TElement Content<TElement>(this TElement presenter, FrameworkElement element = null) where TElement : ElementType
        { presenter.Content = element; return presenter; }

        public static TElement Footer<TElement>(this TElement presenter, FrameworkElement element = null) where TElement : ElementType
        { presenter.Footer = element; return presenter; }

        public static TElement Detail<TElement>(this TElement presenter, FrameworkElement element = null) where TElement : ElementType
        { presenter.Detail = element; return presenter; }

        public static TElement DetailBackgroundColor<TElement>(this TElement presenter, Windows.UI.Color color) where TElement : ElementType
        { presenter.DetailBackgroundColor = color; return presenter; }

        public static TElement DetailBackgroundColor<TElement>(this TElement presenter, uint hexColor) where TElement : ElementType
        { presenter.DetailBackgroundColor = ColorExtensions.ColorFromUint(hexColor); return presenter; }

        public static TElement DetailBackgroundColor<TElement>(this TElement presenter, string color) where TElement : ElementType
        { presenter.DetailBackgroundColor = ColorExtensions.ColorFromString(color); return presenter; }

        public static TElement DetailBorderColor<TElement>(this TElement presenter, Windows.UI.Color color) where TElement : ElementType
        { presenter.DetailBorderColor = color; return presenter; }

        public static TElement DetailBorderColor<TElement>(this TElement presenter, uint hexColor) where TElement : ElementType
        { presenter.DetailBorderColor = ColorExtensions.ColorFromUint(hexColor); return presenter; }

        public static TElement DetailBorderColor<TElement>(this TElement presenter, string color) where TElement : ElementType
        { presenter.DetailBorderColor = ColorExtensions.ColorFromString(color); return presenter; }

        public static TElement DetailBorderWidth<TElement>(this TElement presenter, double value) where TElement : ElementType
        { presenter.DetailBorderWidth = value; return presenter; }

        public static TElement DetailCornerRadius<TElement>(this TElement presenter, double value) where TElement : ElementType
        { presenter.DetailCornerRadius = value; return presenter; }

        public static TElement DrawerAspectRatio<TElement>(this TElement presenter, double aspect) where TElement : ElementType
        { presenter.DrawerAspectRatio = aspect; return presenter; }

        public static TElement Target<TElement>(this TElement presenter, FrameworkElement element) where TElement : ElementType
        { presenter.Target = element; return presenter; }

        public static TElement PopupWidth<TElement>(this TElement presenter, double width) where TElement : ElementType
        { presenter.PopupMinWidth = width; return presenter; }

        public static TElement PopupHeight<TElement>(this TElement presenter, double height) where TElement : ElementType
        { presenter.PopupMinHeight = height; return presenter; }

        public static TElement PopupHorizontalAlignment<TElement>(this TElement presenter, HorizontalAlignment alignment) where TElement : ElementType
        { presenter.PopupHorizontalAlignment = alignment; return presenter; }

        public static TElement PopupVerticalAlignment<TElement>(this TElement presenter, VerticalAlignment alignment) where TElement : ElementType
        { presenter.PopupVerticalAlignment = alignment; return presenter; }

        public static TElement PopOnPageOverlayTouch<TElement>(this TElement presenter, bool enabled = true) where TElement : ElementType
        { presenter.PopOnPageOverlayTouch = enabled; return presenter; }

        public static TElement PageOverlayHitTestVisible<TElement>(this TElement presenter, bool enabled = true) where TElement : ElementType
        { presenter.IsPageOverlayHitTestVisible = enabled; return presenter; }

        public static TElement PageOverlayBrush<TElement>(this TElement presenter, Brush brush) where TElement : ElementType
        { presenter.PageOverlayBrush = brush; return presenter; }

        public static TElement PageOverlayBrush<TElement>(this TElement presenter, Windows.UI.Color color) where TElement : ElementType
        { presenter.PageOverlayBrush = new SolidColorBrush(color); return presenter; }

        public static TElement PageOverlayBrush<TElement>(this TElement presenter, string color) where TElement : ElementType
        { presenter.PageOverlayBrush = new SolidColorBrush(ColorExtensions.ColorFromString(color)); return presenter; }

        public static TElement PageOverlayBrush<TElement>(this TElement presenter, uint hex) where TElement : ElementType
        { presenter.PageOverlayBrush = new SolidColorBrush(ColorExtensions.ColorFromUint(hex)); return presenter; }

    }
}
