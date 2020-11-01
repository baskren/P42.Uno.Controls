using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using ElementType = P42.Uno.Controls.TargetedPopup;


namespace P42.Uno.Controls
{
    public static class TargetedPopupExtensions

    {
		public static TElement Content<TElement>(this TElement element, object value) where TElement : ElementType
		{ element.PopupContent = value; return element; }

		#region Alignment

		public static TElement Center<TElement>(this TElement element) where TElement : ElementType
		{
			element.PopupVerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
			element.PopupHorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
			return element;
		}

		public static TElement Stretch<TElement>(this TElement element) where TElement : ElementType
		{
			element.PopupVerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
			element.PopupHorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
			return element;
		}

		#region Vertical Alignment
		public static TElement VerticalAlignment<TElement>(this TElement element, VerticalAlignment verticalAlignment) where TElement : ElementType
		{ element.PopupVerticalAlignment = verticalAlignment; return element; }

		public static TElement Top<TElement>(this TElement element) where TElement : ElementType
		{ element.PopupVerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top; return element; }

		public static TElement CenterVertical<TElement>(this TElement element) where TElement : ElementType
		{ element.PopupVerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center; return element; }

		public static TElement Bottom<TElement>(this TElement element) where TElement : ElementType
		{ element.PopupVerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom; return element; }

		public static TElement StretchVertical<TElement>(this TElement element) where TElement : ElementType
		{ element.PopupVerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch; return element; }

		#endregion

		#region Horizontal Alignment
		public static TElement HorizontalAlignment<TElement>(this TElement element, HorizontalAlignment horizontalAlignment) where TElement : ElementType
		{ element.PopupHorizontalAlignment = horizontalAlignment; return element; }

		public static TElement Left<TElement>(this TElement element) where TElement : ElementType
		{ element.PopupHorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left; return element; }

		public static TElement CenterHorizontal<TElement>(this TElement element) where TElement : ElementType
		{ element.PopupHorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center; return element; }

		public static TElement Right<TElement>(this TElement element) where TElement : ElementType
		{ element.PopupHorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right; return element; }

		public static TElement StretchHorizontal<TElement>(this TElement element) where TElement : ElementType
		{ element.PopupHorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch; return element; }
		#endregion

		#endregion

		#region Margin

		public static TElement Margin<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PopupMargin = new Thickness(value); return element; }

		public static TElement Margin<TElement>(this TElement element, double horizontal, double vertical) where TElement : ElementType
		{ element.PopupMargin = new Thickness(horizontal, vertical, horizontal, vertical); return element; }

		public static TElement Margin<TElement>(this TElement element, double left, double top, double right, double bottom) where TElement : ElementType
		{ element.PopupMargin = new Thickness(left, top, right, bottom); return element; }

		public static TElement Margin<TElement>(this TElement element, Thickness margin) where TElement : ElementType
		{ element.PopupMargin = margin; return element; }

		#endregion

		#region Padding
		public static TElement Padding<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PopupPadding = new Thickness(value); return element; }

		public static TElement Padding<TElement>(this TElement element, double horizontal, double vertical) where TElement : ElementType
		{ element.PopupPadding = new Thickness(horizontal, vertical, horizontal, vertical); return element; }

		public static TElement Padding<TElement>(this TElement element, double left, double top, double right, double bottom) where TElement : ElementType
		{ element.PopupPadding = new Thickness(left, top, right, bottom); return element; }

		public static TElement Padding<TElement>(this TElement element, Thickness padding) where TElement : ElementType
		{ element.PopupPadding = padding; return element; }
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

		public static TElement PreferredPointerDirection<TElement>(this TElement element, PointerDirection value) where TElement : ElementType
		{ element.PreferredPointerDirection = value; return element; }

		public static TElement FallbackPointerDirection<TElement>(this TElement element, PointerDirection value) where TElement : ElementType
		{ element.FallbackPointerDirection = value; return element; }

		public static TElement PointerLength<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PointerLength = value; return element; }

		public static TElement PointerTipRadius<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PointerTipRadius = value; return element; }
        #endregion

        #region LightDismiss Properties
        public static TElement IsLightDismissEnabled<TElement>(this TElement element, bool value = true) where TElement : ElementType
		{ element.IsLightDismissEnabled = value; return element; }

		public static TElement LightDismissOverlayMode<TElement>(this TElement element, LightDismissOverlayMode value) where TElement : ElementType
		{ element.LightDismissOverlayMode = value; return element; }

		public static TElement LightDismissOverlayBrush<TElement>(this TElement element, Brush value) where TElement : ElementType
		{ element.LightDismissOverlayBrush = value; return element; }

		public static TElement LightDismissOverlayBrush<TElement>(this TElement element, Color value) where TElement : ElementType
		{ element.LightDismissOverlayBrush = new SolidColorBrush(value); return element; }

		public static TElement LightDismissOverlayBrush<TElement>(this TElement element, string value) where TElement : ElementType
		{ element.LightDismissOverlayBrush = new SolidColorBrush(P42.Utils.Uno.ColorExtensions.ColorFromString(value)); return element; }
        #endregion


    }
}
