using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    public partial class AlignedPlacementPanel : Panel
    {
        Button _moreButton = new Button
        {
            Content = "...",
        };
        List<FrameworkElement> MoreChildren = new List<FrameworkElement>();

        protected override Size MeasureOverride(Size availableSize)
        {
            var size = new Size(availableSize.Width, 40);
            foreach (var child in Children)
                child.Measure(size);
            _moreButton.Measure(size);
            return base.MeasureOverride(size);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            MoreChildren.Clear();

            List<FrameworkElement> leftChildren = new List<FrameworkElement>();
            List<FrameworkElement> centerChildren = new List<FrameworkElement>();
            List<FrameworkElement> rightChildren = new List<FrameworkElement>();

            var width = 0.0;
            foreach (var child in Children)
            {
                if (child is FrameworkElement element)
                {
                    if (element.HorizontalAlignment == HorizontalAlignment.Left)
                        leftChildren.Add(element);
                    else if (element.HorizontalAlignment == HorizontalAlignment.Right)
                        rightChildren.Add(element);
                    else
                        centerChildren.Add(element);
                    width += element.DesiredSize.Width;
                }
                else
                    throw new Exception("only FrameworkElement is allowed to be a child of " + GetType() + ".  ["+child+"] is not a FrameworkElement.");
            }

            if (width > finalSize.Width)
                centerChildren.Insert(0, _moreButton);
            var leftWidth = 0.0;
            var rightWidth = 0.0;
            var centerWidth = 0.0;
            var maxSectionCount = Math.Max(leftChildren.Count, Math.Max(rightChildren.Count, centerChildren.Count));
            for (int i=0;i< maxSectionCount;i++)
            {

                if (i<leftChildren.Count)
                {
                    var element = leftChildren[i];
                    if (leftWidth + centerWidth + rightWidth + element.DesiredSize.Width < finalSize.Width)
                    {
                        element.Arrange(new Rect(new Point(leftWidth, (finalSize.Height - element.DesiredSize.Height)/2.0), element.DesiredSize));
                        leftWidth += element.DesiredSize.Width;
                    }
                    else
                    {
                        leftChildren.Remove(element);
                        MoreChildren.Add(element);
                    }
                }
                if (i<rightChildren.Count)
                {
                    var element = rightChildren[i];
                    if (leftWidth + centerWidth + rightWidth +  element.DesiredSize.Width < finalSize.Width)
                    {
                        rightWidth += element.DesiredSize.Width;
                        element.Arrange(new Rect(new Point(finalSize.Width-rightWidth, (finalSize.Height - element.DesiredSize.Height) / 2.0), element.DesiredSize));
                    }
                    else
                    {
                        rightChildren.Remove(element);
                        MoreChildren.Add(element);
                    }
                }
                if (i<centerChildren.Count)
                {
                    var element = centerChildren[i];
                    if (leftWidth + centerWidth + rightWidth + element.DesiredSize.Width < finalSize.Width)
                    {
                        centerWidth += element.DesiredSize.Width;
                    }
                    else
                    {
                        centerChildren.Remove(element);
                        MoreChildren.Add(element);
                    }
                }
            }

            var gap = finalSize.Width - rightWidth - centerWidth - leftWidth;
            var centerX = leftWidth + gap / 2;
            foreach (var element in centerChildren)
            {
                element.Arrange(new Rect(new Point(centerX, (finalSize.Height - element.DesiredSize.Height) / 2.0), element.DesiredSize));
                centerX += element.DesiredSize.Width;
            }

            return base.ArrangeOverride(finalSize);
        }
    }
}
