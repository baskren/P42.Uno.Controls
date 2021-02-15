using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Shapes;

namespace P42.Uno.Controls
{
    partial class SegmentPanel : Panel
    {
        #region Properties

        #region Orientation Property
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            nameof(Orientation),
            typeof(Orientation),
            typeof(SegmentPanel),
            new PropertyMetadata(default(Orientation), new PropertyChangedCallback(OnOrientationChanged))
        );
        protected static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SegmentPanel SegmentPanel)
            {
                SegmentPanel.InvalidateMeasure();
            }
        }
        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }
        #endregion Orientation Property

        public bool ExceedsAvailableSpace { get; private set; }

        #region SeparatorThickness Property
        public static readonly DependencyProperty SeparatorThicknessProperty = DependencyProperty.Register(
            nameof(SeparatorThickness),
            typeof(double),
            typeof(SegmentPanel),
            new PropertyMetadata(1.0, new PropertyChangedCallback(OnSeparatorWidthChanged))
        );
        protected static void OnSeparatorWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SegmentPanel SegmentPanel)
            {
                SegmentPanel.InvalidateMeasure();
            }
        }
        public double SeparatorThickness
        {
            get => (double)GetValue(SeparatorThicknessProperty);
            set => SetValue(SeparatorThicknessProperty, value);
        }
        #endregion SeparatorThickness Property


        #endregion

        Size GetMaxCellSize()
        {
            var maxWidth = 0.0;
            var maxHeight = 0.0;
            foreach (var uiElement in Children)
            {
                if (!(uiElement is Rectangle))
                {
                    maxWidth = Math.Max(uiElement.DesiredSize.Width, maxWidth);
                    maxHeight = Math.Max(uiElement.DesiredSize.Height, maxHeight);
                }
            }
            return new Size(maxWidth, maxHeight);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (var uiElement in Children)
                uiElement.Measure(availableSize);
            var maxCellSize = GetMaxCellSize();
            var separators = Children.Count(child => child is Rectangle);

            var width = Orientation == Orientation.Horizontal
                ? Children.Count * maxCellSize.Width + SeparatorThickness * separators
                : maxCellSize.Width;
            var height = Orientation == Orientation.Vertical
                ? Children.Count * maxCellSize.Height + SeparatorThickness * separators
                : maxCellSize.Height;

            if (width <= 0 || height <= 0)
                return new Size(width, height);

            ExceedsAvailableSpace = Orientation == Orientation.Horizontal
                ? width > availableSize.Width
                : height > availableSize.Height;

            return new Size(
                HorizontalAlignment == HorizontalAlignment.Stretch 
                    ? double.IsInfinity(availableSize.Width)
                        ? width
                        : availableSize.Width
                    : width, 
                VerticalAlignment == VerticalAlignment.Stretch
                    ? double.IsInfinity(availableSize.Height)
                        ? height
                        : availableSize.Height
                    : height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            //System.Diagnostics.Debug.WriteLine("SegmentPanel.ArrangeOverride finalSize ["+finalSize+"]");
            //System.Diagnostics.Debug.WriteLine("SegmentPanel.ArrangeOverride Margin ["+Margin+"]");

            var separators = Children.Count(child => child is Rectangle);
            var elements = Children.Count - separators;
            var maxCellSize = GetMaxCellSize();
            var x = 0.0;
            var y = 0.0;
            if (Orientation == Orientation.Horizontal)
            {
                if (HorizontalAlignment == HorizontalAlignment.Stretch)
                    maxCellSize.Width = (finalSize.Width - separators * SeparatorThickness) / elements;
                else if (HorizontalAlignment == HorizontalAlignment.Center)
                {
                    var width = maxCellSize.Width * elements + separators * SeparatorThickness;
                    x = (finalSize.Width - width) / 2.0;
                }
                else if (HorizontalAlignment == HorizontalAlignment.Right)
                {
                    var width = maxCellSize.Width * elements + separators * SeparatorThickness;
                    x = finalSize.Width - width;
                }

                var height = maxCellSize.Height;
                if (VerticalAlignment == VerticalAlignment.Stretch)
                {
                    height = finalSize.Height;
                    y = 0;  //WTF ?  SHould not be necessary, yet required for UWP!?!?
                }
                else if (VerticalAlignment == VerticalAlignment.Center)
                    y = (finalSize.Height - maxCellSize.Height) / 2.0;
                else if (VerticalAlignment == VerticalAlignment.Bottom)
                    y = finalSize.Height - maxCellSize.Height;

                foreach (var uiElement in Children)
                {
                    if (uiElement is Rectangle rectangle)
                    {
                        rectangle.Arrange(new Rect(x, y, SeparatorThickness, height));
                        x += SeparatorThickness;
                    }
                    else
                    {
                        var r = new Rect(x, y, maxCellSize.Width, height);
                        //System.Diagnostics.Debug.WriteLine("SegmentPanel.ArrangeOverride ["+((Segment)uiElement).Content+"] ["+r+"]");
                        uiElement.Arrange(r);
                        x += maxCellSize.Width;
                    }
                }
                return new Size(x, height);
            }
            else
            {
                if (VerticalAlignment == VerticalAlignment.Stretch)
                    maxCellSize.Height = (finalSize.Height - separators * SeparatorThickness) / elements;
                else if (VerticalAlignment == VerticalAlignment.Center)
                {
                    var height = maxCellSize.Height * elements + separators * SeparatorThickness;
                    y = (finalSize.Height - height) / 2.0;
                }
                else if (VerticalAlignment == VerticalAlignment.Bottom)
                {
                    var height = maxCellSize.Height * elements + separators * SeparatorThickness;
                    y = finalSize.Height - height;
                }

                var width = maxCellSize.Width;
                if (HorizontalAlignment == HorizontalAlignment.Stretch)
                    width = finalSize.Width;
                else if (HorizontalAlignment == HorizontalAlignment.Center)
                    x = (finalSize.Width - maxCellSize.Width) / 2.0;
                else if (HorizontalAlignment == HorizontalAlignment.Right)
                    x = finalSize.Width - maxCellSize.Width;

                foreach (var uiElement in Children)
                {
                    if (uiElement is Rectangle rect)
                    {
                        rect.Arrange(new Rect(x, y, width, SeparatorThickness));
                        y += SeparatorThickness;
                    }
                    else
                    {
                        uiElement.Arrange(new Rect(x, y, width, maxCellSize.Height));
                        y += maxCellSize.Height;
                    }
                }
                return new Size(x, y);
            }
        }
    }
}
