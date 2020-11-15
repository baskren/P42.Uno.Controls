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
        List<UIElement> ExtraChildren = new List<UIElement>();

        TargetedPopup _popup;

        public AlignedPlacementPanel()
        {
            Children.Add(_moreButton);
            _moreButton.Click += OnMoreButtonClicked;
        }

        async void OnMoreButtonClicked(object sender, RoutedEventArgs e)
        {
            var stack = new StackPanel();
            var w = stack.Margin.Horizontal() + stack.Padding.Horizontal();
            var h = stack.Margin.Vertical() + stack.Padding.Vertical();
            foreach (var child in ExtraChildren)
            {
                w = Math.Max(w, child.DesiredSize.Width);
                h += child.DesiredSize.Height + 3;
                Children.Remove(child);
                if (child is Button b)
                    b.Click += OnMoreButtonChildClicked;
            }
            stack.Spacing(3)
                //.Margin(0)
                //.Padding(0)
                .Children(
                    ExtraChildren
                );
            //stack.Width = w;
            //stack.Height = h;
            _popup = await P42.Uno.Controls.TargetedPopup.CreateAsync(_moreButton, stack);
            _popup.Padding(2);
            await _popup.WaitForPoppedAsync();
            foreach (var child in ExtraChildren)
            {
                if (child is Button b)
                    b.Click -= OnMoreButtonChildClicked;
                stack.Children.Remove(child);
                Children.Add(child);
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

        bool _arranging;
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_arranging)
                return finalSize;
            _arranging = true;
            Clip = new RectangleGeometry { Rect = new Rect(new Point(), finalSize) };

            System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange ENTER");
            //foreach (var child in MoreChildren)
            //    Children.Add(child);
            //MoreChildren.Clear();

            var leftChildren = new List<FrameworkElement>();
            var centerChildren = new List<FrameworkElement>();
            var rightChildren = new List<FrameworkElement>();

            var width = 0.0;
            //var children = new List<UIElement>(Children);
            //children.AddRange(MoreChildren);
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
            bool leftDone = false, rightDone = false, centerDone = false;
            for (int i=0;i< maxSectionCount;i++)
            {
                if (i<leftChildren.Count)
                {
                    var element = leftChildren[i];
                    if (!leftDone && leftWidth + centerWidth + rightWidth + element.DesiredSize.Width < finalSize.Width)
                    {
                        if (ExtraChildren.Contains(element))
                            ExtraChildren.Remove(element);
                        System.Diagnostics.Debug.Write("AlignedPlacementPanel.Arrange left["+i+"] ...");
                        element.Arrange(new Rect(new Point(leftWidth, (finalSize.Height - element.DesiredSize.Height)/2.0), element.DesiredSize));
                        System.Diagnostics.Debug.WriteLine(" ! ");
                        leftWidth += element.DesiredSize.Width;
                    }
                    else
                    {
                        leftDone = true;
                        leftChildren.Remove(element);
                        if (!ExtraChildren.Contains(element))
                            ExtraChildren.Add(element);
                    }
                }
                if (i<rightChildren.Count)
                {
                    var element = rightChildren[i];
                    if (!rightDone && leftWidth + centerWidth + rightWidth +  element.DesiredSize.Width < finalSize.Width)
                    {
                        if (ExtraChildren.Contains(element))
                            ExtraChildren.Remove(element);
                        rightWidth += element.DesiredSize.Width;
                        System.Diagnostics.Debug.Write("AlignedPlacementPanel.Arrange right[" + i + "] ...");
                        element.Arrange(new Rect(new Point(finalSize.Width-rightWidth, (finalSize.Height - element.DesiredSize.Height) / 2.0), element.DesiredSize));
                        System.Diagnostics.Debug.WriteLine(" ! ");
                    }
                    else
                    {
                        rightDone = true;
                        rightChildren.Remove(element);
                        if (!ExtraChildren.Contains(element))
                            ExtraChildren.Add(element);
                    }
                }
                if (i<centerChildren.Count)
                {
                    var element = centerChildren[i];
                    System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange center[" + i + "] size:[" + element.DesiredSize + "] centerDone: "+ centerDone);
                    if (!centerDone && leftWidth + centerWidth + rightWidth + element.DesiredSize.Width < finalSize.Width)
                    {
                        if (ExtraChildren.Contains(element))
                            ExtraChildren.Remove(element);
                        System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange KEEP");
                        centerWidth += element.DesiredSize.Width;
                    }
                    else
                    {
                        centerDone = true;
                        System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange REMOVE");
                        centerChildren.Remove(element);
                        if (!ExtraChildren.Contains(element))
                            ExtraChildren.Add(element);
                    }
                }
            }

            var gap = finalSize.Width - rightWidth - centerWidth - leftWidth;
            var centerX = leftWidth + gap / 2;
            if (!centerChildren.Contains(_moreButton))
                _moreButton.Arrange(new Rect(new Point(finalSize.Width + 10, finalSize.Height +10), _moreButton.DesiredSize));
            foreach (var element in centerChildren)
            {
                System.Diagnostics.Debug.Write("AlignedPlacementPanel.Arrange center[" + element + "] ...");
                element.Arrange(new Rect(new Point(centerX, (finalSize.Height - element.DesiredSize.Height) / 2.0), element.DesiredSize));
                System.Diagnostics.Debug.WriteLine(" ! ");
                centerX += element.DesiredSize.Width;
            }


            foreach (var child in ExtraChildren)
            {
                child.Arrange(new Rect(new Point(finalSize.Width, 0), child.DesiredSize));
                //if (Children.Contains(child))
                //Children.Remove(child);
            }

            _arranging = false;
            System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange EXIT");
            return finalSize;
        }
    }
}
