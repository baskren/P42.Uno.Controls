using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace P42.Uno.Controls
{
    public static class PopupMarkupExtensions
    {
		#region BubbleBorder

		#region Alignment

		public static TElement Center<TElement>(this TElement element) where TElement : BubbleBorder
		{
			element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
			element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
			return element;
		}

		public static TElement Stretch<TElement>(this TElement element) where TElement : BubbleBorder
		{
			element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
			element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
			return element;
		}

		#region Vertical Alignment
		public static TElement VerticalAlignment<TElement>(this TElement element, VerticalAlignment verticalAlignment) where TElement : BubbleBorder
		{ element.VerticalAlignment = verticalAlignment; return element; }

		public static TElement Top<TElement>(this TElement element) where TElement : BubbleBorder
		{ element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top; return element; }

		public static TElement CenterVertical<TElement>(this TElement element) where TElement : BubbleBorder
		{ element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center; return element; }

		public static TElement Bottom<TElement>(this TElement element) where TElement : BubbleBorder
		{ element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom; return element; }

		public static TElement StretchVertical<TElement>(this TElement element) where TElement : BubbleBorder
		{ element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch; return element; }

		#endregion

		#region Horizontal Alignment
		public static TElement HorizontalAlignment<TElement>(this TElement element, HorizontalAlignment horizontalAlignment) where TElement : BubbleBorder
		{ element.HorizontalAlignment = horizontalAlignment; return element; }

		public static TElement Left<TElement>(this TElement element) where TElement : BubbleBorder
		{ element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left; return element; }

		public static TElement CenterHorizontal<TElement>(this TElement element) where TElement : BubbleBorder
		{ element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center; return element; }

		public static TElement Right<TElement>(this TElement element) where TElement : BubbleBorder
		{ element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right; return element; }

		public static TElement StretchHorizontal<TElement>(this TElement element) where TElement : BubbleBorder
		{ element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch; return element; }
		#endregion


		#endregion

		public static TElement PointerBias<TElement>(this TElement element, double value) where TElement : BubbleBorder
		{ element.PointerBias = value; return element; }

		public static TElement PointerLength<TElement>(this TElement element, double value) where TElement : BubbleBorder
		{ element.PointerLength = value; return element; }

		public static TElement PointerTipRadius<TElement>(this TElement element, double value) where TElement : BubbleBorder
		{ element.PointerTipRadius = value; return element; }

		public static TElement PointerAxialPosition<TElement>(this TElement element, double value) where TElement : BubbleBorder
		{ element.PointerAxialPosition = value; return element; }

		public static TElement PointerDirection<TElement>(this TElement element, PointerDirection value) where TElement : BubbleBorder
		{ element.PointerDirection = value; return element; }

		public static TElement PointerCornerRadius<TElement>(this TElement element, double value) where TElement : BubbleBorder
		{ element.PointerCornerRadius = value; return element; }

		public static TElement HasShadow<TElement>(this TElement element, bool value = true) where TElement : BubbleBorder
		{ element.HasShadow = value; return element; }

		public static TElement Size<TElement>(this TElement element, Size value) where TElement : BubbleBorder
		{ element.Size = value; return element; }

		#endregion

		#region Margin

		public static TElement Margin<TElement>(this TElement element, double value) where TElement : BubbleBorder
		{ element.Margin = new Thickness(value); return element; }

		public static TElement Margin<TElement>(this TElement element, double horizontal, double vertical) where TElement : BubbleBorder
		{ element.Margin = new Thickness(horizontal, vertical, horizontal, vertical); return element; }

		public static TElement Margin<TElement>(this TElement element, double left, double top, double right, double bottom) where TElement : BubbleBorder
		{ element.Margin = new Thickness(left, top, right, bottom); return element; }

		public static TElement Margin<TElement>(this TElement element, Thickness margin) where TElement : BubbleBorder
		{ element.Margin = margin; return element; }

		#endregion


	}
}
