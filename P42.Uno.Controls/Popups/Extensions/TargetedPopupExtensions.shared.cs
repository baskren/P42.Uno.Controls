using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using ElementType = P42.Uno.Controls.TargetedPopup;
using P42.Uno.Markup;

namespace P42.Uno.Controls
{
    public static class TargetedPopupExtensions

    {
		public static TElement Content<TElement>(this TElement element, object value) where TElement : ElementType
		{ element.Content = value; return element; }

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
		public static TElement VerticalAlignment<TElement>(this TElement element, VerticalAlignment verticalAlignment) where TElement : ElementType
		{ element.VerticalAlignment = verticalAlignment; return element; }

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
		public static TElement HorizontalAlignment<TElement>(this TElement element, HorizontalAlignment horizontalAlignment) where TElement : ElementType
		{ element.HorizontalAlignment = horizontalAlignment; return element; }

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

		public static TElement HasShadow<TElement>(this TElement element, bool value = true) where TElement : ElementType
		{ element.HasShadow = value; return element; }

		public static TElement PopAfter<TElement>(this TElement element, TimeSpan value) where TElement : ElementType
		{ element.PopAfter = value; return element; }

		public static TElement Target<TElement>(this TElement element, UIElement value) where TElement : ElementType
		{ element.Target = value; return element; }

        #region Pointer Properties
        public static TElement PointerBias<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PointerBias = value; return element; }

		public static TElement PointerCornerRadius<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PointerCornerRadius = value; return element; }

        #region Pointer Direction
        public static TElement PreferredPointerDirection<TElement>(this TElement element, PointerDirection value) where TElement : ElementType
		{ element.PreferredPointerDirection = value; return element; }

		public static TElement FallbackPointerDirection<TElement>(this TElement element, PointerDirection value) where TElement : ElementType
		{ element.FallbackPointerDirection = value; return element; }

		public static TElement PreferredPointerDown<TElement>(this TElement element) where TElement : ElementType
		{ element.PreferredPointerDirection = P42.Uno.Controls.PointerDirection.Down; return element; }

		public static TElement PreferredPointerUp<TElement>(this TElement element) where TElement : ElementType
		{ element.PreferredPointerDirection = P42.Uno.Controls.PointerDirection.Up; return element; }

		public static TElement PreferredPointerLeft<TElement>(this TElement element) where TElement : ElementType
		{ element.PreferredPointerDirection = P42.Uno.Controls.PointerDirection.Left; return element; }

		public static TElement PreferredPointerRight<TElement>(this TElement element) where TElement : ElementType
		{ element.PreferredPointerDirection = P42.Uno.Controls.PointerDirection.Right; return element; }

		public static TElement PreferredPointerHorizontal<TElement>(this TElement element) where TElement : ElementType
		{ element.PreferredPointerDirection = P42.Uno.Controls.PointerDirection.Horizontal; return element; }

		public static TElement PreferredPointerVertical<TElement>(this TElement element) where TElement : ElementType
		{ element.PreferredPointerDirection = P42.Uno.Controls.PointerDirection.Vertical; return element; }

		public static TElement PreferredPointerAny<TElement>(this TElement element) where TElement : ElementType
		{ element.PreferredPointerDirection = P42.Uno.Controls.PointerDirection.Any; return element; }

		public static TElement FallbackPointerDown<TElement>(this TElement element) where TElement : ElementType
		{ element.FallbackPointerDirection = P42.Uno.Controls.PointerDirection.Down; return element; }

		public static TElement FallbackPointerUp<TElement>(this TElement element) where TElement : ElementType
		{ element.FallbackPointerDirection = P42.Uno.Controls.PointerDirection.Up; return element; }

		public static TElement FallbackPointerLeft<TElement>(this TElement element) where TElement : ElementType
		{ element.FallbackPointerDirection = P42.Uno.Controls.PointerDirection.Left; return element; }

		public static TElement FallbackPointerRight<TElement>(this TElement element) where TElement : ElementType
		{ element.FallbackPointerDirection = P42.Uno.Controls.PointerDirection.Right; return element; }

		public static TElement FallbackPointerHorizontal<TElement>(this TElement element) where TElement : ElementType
		{ element.FallbackPointerDirection = P42.Uno.Controls.PointerDirection.Horizontal; return element; }

		public static TElement FallbackPointerVertical<TElement>(this TElement element) where TElement : ElementType
		{ element.FallbackPointerDirection = P42.Uno.Controls.PointerDirection.Vertical; return element; }

		public static TElement FallbackPointerAny<TElement>(this TElement element) where TElement : ElementType
		{ element.FallbackPointerDirection = P42.Uno.Controls.PointerDirection.Any; return element; }
        #endregion

        public static TElement PointerLength<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PointerLength = value; return element; }

		public static TElement PointerTipRadius<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PointerTipRadius = value; return element; }
        #endregion

        #region LightDismiss Properties
        /*
        public static TElement IsLightDismissEnabled<TElement>(this TElement element, bool value = true) where TElement : ElementType
		{ element.IsLightDismissEnabled = value; return element; }

		public static TElement LightDismissOverlayMode<TElement>(this TElement element, LightDismissOverlayMode value) where TElement : ElementType
		{ element.LightDismissOverlayMode = value; return element; }
		public static TElement LightDismissOverlayBrush<TElement>(this TElement element, Brush value) where TElement : ElementType
		{ element.LightDismissOverlayBrush = value; return element; }

		public static TElement LightDismissOverlayBrush<TElement>(this TElement element, Color value) where TElement : ElementType
		{ element.LightDismissOverlayBrush = new SolidColorBrush(value); return element; }

		public static TElement LightDismissOverlayBrush<TElement>(this TElement element, string color) where TElement : ElementType
		{ element.LightDismissOverlayBrush = new SolidColorBrush(P42.Uno.Markup.ColorExtensions.ColorFromString(color)); return element; }
		*/
        #endregion


        #region PageOverlay
        public static TElement PageOverlay<TElement>(this TElement element, Brush brush) where TElement : ElementType
        { element.PageOverlayBrush = brush; return element; }

        public static TElement PageOverlay<TElement>(this TElement element, Color color) where TElement : ElementType
        { element.PageOverlayBrush = new SolidColorBrush(color); return element; }

        public static TElement PageOverlay<TElement>(this TElement element, string color) where TElement : ElementType
        { element.PageOverlayBrush = new SolidColorBrush(ColorExtensions.ColorFromString(color)); return element; }

        public static TElement PageOverlay<TElement>(this TElement element, uint hex) where TElement : ElementType
        { element.PageOverlayBrush = new SolidColorBrush(ColorExtensions.ColorFromUint(hex)); return element; }
        #endregion

    }
}
