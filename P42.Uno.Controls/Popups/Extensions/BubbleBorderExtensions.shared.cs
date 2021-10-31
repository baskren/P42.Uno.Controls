using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using ElementType = P42.Uno.Controls.BubbleBorder;

namespace P42.Uno.Controls
{
    public static class BubbleBorderExtensions

    {
        #region BubbleBorder

        #region Content
		public static TElement Content<TElement>(this TElement element, object content) where TElement : ElementType
        {
			element.Content = content;
			return element;
        }
        #endregion

        #region Padding
		public static TElement Padding<TElement>(this TElement element, Thickness thickness) where TElement : ElementType
        {
			element.Padding = thickness;
			return element;
        }
		public static TElement Padding<TElement>(this TElement element, double t) where TElement : ElementType
		{
			element.Padding = new Thickness(t);
			return element;
		}
		public static TElement Padding<TElement>(this TElement element, double h, double v) where TElement : ElementType
		{
			element.Padding = new Thickness(h, v, h, v);
			return element;
		}
		public static TElement Padding<TElement>(this TElement element, double l, double t, double r, double b) where TElement : ElementType
		{
			element.Padding = new Thickness(l, t, r, b);
			return element;
		}
		#endregion

		#region Alignment

		public static TElement Center<TElement>(this TElement element) where TElement : ElementType
		{
			element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
			element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
			return element;
		}

		public static TElement Stretch<TElement>(this TElement element) where TElement : ElementType
		{
			element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
			element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
			return element;
		}

		#region Vertical Alignment
		public static TElement VerticalAlignment<TElement>(this TElement element, VerticalAlignment verticalAlignment) where TElement : ElementType
		{ element.VerticalAlignment = verticalAlignment; return element; }

		public static TElement Top<TElement>(this TElement element) where TElement : ElementType
		{ element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top; return element; }

		public static TElement CenterVertical<TElement>(this TElement element) where TElement : ElementType
		{ element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center; return element; }

		public static TElement Bottom<TElement>(this TElement element) where TElement : ElementType
		{ element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom; return element; }

		public static TElement StretchVertical<TElement>(this TElement element) where TElement : ElementType
		{ element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch; return element; }

		#endregion

		#region Horizontal Alignment
		public static TElement HorizontalAlignment<TElement>(this TElement element, HorizontalAlignment horizontalAlignment) where TElement : ElementType
		{ element.HorizontalAlignment = horizontalAlignment; return element; }

		public static TElement Left<TElement>(this TElement element) where TElement : ElementType
		{ element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left; return element; }

		public static TElement CenterHorizontal<TElement>(this TElement element) where TElement : ElementType
		{ element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center; return element; }

		public static TElement Right<TElement>(this TElement element) where TElement : ElementType
		{ element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right; return element; }

		public static TElement StretchHorizontal<TElement>(this TElement element) where TElement : ElementType
		{ element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch; return element; }
		#endregion

		#endregion

		public static TElement PointerBias<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PointerBias = value; return element; }

		public static TElement PointerLength<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PointerLength = value; return element; }

		public static TElement PointerTipRadius<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PointerTipRadius = value; return element; }

		public static TElement PointerAxialPosition<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PointerAxialPosition = value; return element; }

		public static TElement PointerDirection<TElement>(this TElement element, PointerDirection value) where TElement : ElementType
		{ element.PointerDirection = value; return element; }

		public static TElement PointerCornerRadius<TElement>(this TElement element, double value) where TElement : ElementType
		{ element.PointerCornerRadius = value; return element; }

		public static TElement HasShadow<TElement>(this TElement element, bool value = true) where TElement : ElementType
		{ element.HasShadow = value; return element; }

		public static TElement Size<TElement>(this TElement element, Size value) where TElement : ElementType
		{ element.Size = value; return element; }

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

		#endregion



	}
}
