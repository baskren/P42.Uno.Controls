using P42.Uno.Markup;
using P42.Utils.Uno;
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

namespace P42.Uno.Controls
{
    public partial class AlignedPlacementPanel : Panel
    {
        Button _moreButton = new Button
        {
            Content = "...",
            Background = Colors.Transparent.ToBrush()
        };
        List<UIElement> MoreChildren = new List<UIElement>();

        TargetedPopup _popup;

        public AlignedPlacementPanel()
        {
            Children.Add(_moreButton);
            _moreButton.Click += OnMoreButtonClicked;
        }

        async void OnMoreButtonClicked(object sender, RoutedEventArgs e)
        {
            var stack = new StackPanel()
                .Spacing(3)
                //.Margin(0)
                //.Padding(0)
                .Children(
                    MoreChildren
                );
            var w = stack.Margin.Horizontal() + stack.Padding.Horizontal();
            var h = stack.Margin.Vertical() + stack.Padding.Vertical();
            foreach (var child in MoreChildren)
            {
                if (child is Button b)
                    b.Click += OnMoreButtonChildClicked;
                w = Math.Max(w, child.DesiredSize.Width);
                h += child.DesiredSize.Height + 3;
            }
            stack.Width = w;
            stack.Height = h;
            _popup = await P42.Uno.Controls.TargetedPopup.CreateAsync(_moreButton, stack);
            _popup.Padding(2);
            await _popup.WaitForPoppedAsync();
            foreach (var child in MoreChildren)
            {
                if (child is Button b)
                    b.Click -= OnMoreButtonChildClicked;
                stack.Children.Remove(child);
            }
            _popup = null;
        }

        async void OnMoreButtonChildClicked(object sender, RoutedEventArgs e)
        {
            if (_popup is TargetedPopup p)
                await p.PopAsync();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (availableSize.IsZero())
                return availableSize;
            var size = new Size(availableSize.Width, 40);
            foreach (var child in Children)
                child.Measure(size);
            _moreButton.Measure(size);
            return size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (var child in MoreChildren)
                Children.Add(child);
            MoreChildren.Clear();

            var leftChildren = new List<FrameworkElement>();
            var centerChildren = new List<FrameworkElement>();
            var rightChildren = new List<FrameworkElement>();

            var width = 0.0;
            foreach (var child in Children)
            {
                if (child == _moreButton)
                    continue;
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

            var leftWidth = 0.0;
            var rightWidth = 0.0;
            var centerWidth = 0.0;
            if (width > finalSize.Width)
            {
                centerChildren.Insert(0, _moreButton);
            }
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

            foreach (var child in MoreChildren)
                Children.Remove(child);

            return finalSize;
        }
    }
}
