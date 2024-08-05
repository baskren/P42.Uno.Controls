using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace P42.Uno.Controls
{
    [Microsoft.UI.Xaml.Data.Bindable]
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    public partial class AlignedPlacementPanel : Panel
    {
        #region Spacing Property
        public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
            nameof(Spacing),
            typeof(double),
            typeof(AlignedPlacementPanel),
            new PropertyMetadata(default(double))
        );
        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }
        #endregion Spacing Property

        #region Padding Property
        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(AlignedPlacementPanel),
            new PropertyMetadata(default(Thickness))
        );
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        #endregion Padding Property

        Button _moreButton = new Button
        {
            Content = "...",
            Background = Colors.Transparent.ToBrush(),
            Margin = new Thickness(0)
        };
        List<UIElement> ExtraChildren = new List<UIElement>();

        TargetedPopup _popup;

        public AlignedPlacementPanel()
        {
            //Background = SystemColors.BaseLow.ToBrush();
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
                return new Size(Math.Min(availableSize.Width, 50), Math.Min(availableSize.Height, 50));
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

            //System.Diagnostics.Debug.WriteLine($"AlignedPlacementPanel.ArrangeOverride({finalSize})");
            Clip = new RectangleGeometry { Rect = new Rect(new Point(), finalSize) };

            //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange ENTER");
            //foreach (var child in MoreChildren)
            //    Children.Add(child);
            //MoreChildren.Clear();

            var leftChildren = new List<FrameworkElement>();
            var centerChildren = new List<FrameworkElement>();
            var rightChildren = new List<FrameworkElement>();

            var width = Padding.Horizontal();
            //var children = new List<UIElement>(Children);
            //children.AddRange(MoreChildren);
            int childIndex = -1;
            foreach (var child in Children)
            {
                childIndex++;

                if (child == _moreButton)
                    continue;

                if (childIndex > 0)
                    width += Spacing;

                if (child is FrameworkElement element)
                {
                    if (element.HorizontalAlignment == HorizontalAlignment.Left)
                        leftChildren.Add(element);
                    else if (element.HorizontalAlignment == HorizontalAlignment.Right)
                        rightChildren.Add(element);
                    else
                        centerChildren.Add(element);
                    width += element.DesiredSize.Width;
                    //if (element is Button button)
                    //    System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.ArrangeOverride: element: " + button.Content);
                    //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.ArrangeOverride: element["+childIndex+"].DesiredWidth: " + element.DesiredSize.Width);
                }
                else
                    throw new Exception("only FrameworkElement is allowed to be a child of " + GetType() + ".  ["+child+"] is not a FrameworkElement.");
            }

            var leftWidth = Padding.Left;
            var rightWidth = Padding.Right;
            var centerWidth = 0.0;
            if (width > finalSize.Width)
            {
                centerChildren.Insert(0, _moreButton);
                //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange : width > finalSize.Width  : centerChildrenCount=["+centerChildren.Count+"]");
            }
            var maxSectionCount = Math.Max(leftChildren.Count, Math.Max(rightChildren.Count, centerChildren.Count));
            //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrage maxSectionCount["+maxSectionCount+"]");
            bool leftDone = false, rightDone = false, centerDone = false;
            for (int i=0;i< maxSectionCount;i++)
            {
                //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange i["+i+"] LEFT");
                if (i<leftChildren.Count)
                {
                    var element = leftChildren[i];
                    //System.Diagnostics.Debug.WriteLine($"AlignedPlacementPanel left: left[{leftWidth}] center[{centerWidth}] right[{rightWidth}]");
                    if (!leftDone && leftWidth + centerWidth + rightWidth + element.DesiredSize.Width < finalSize.Width)
                    {
                        if (ExtraChildren.Contains(element))
                            ExtraChildren.Remove(element);
                        //System.Diagnostics.Debug.Write("AlignedPlacementPanel.Arrange left["+i+"] ...");
                        element.Arrange(new Rect(new Point(leftWidth, (finalSize.Height - element.DesiredSize.Height)/2.0), element.DesiredSize));
                        //System.Diagnostics.Debug.WriteLine(" ! ");
                        leftWidth += element.DesiredSize.Width;
                        leftWidth += Spacing;  // always add spacing to LEFT
                        //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel left: element.DesiredSize: " + element.DesiredSize);
                    }
                    else
                    {
                        leftDone = true;
                        while (leftChildren.Count > i)
                        {
                            element = leftChildren[i];
                            leftChildren.Remove(element);
                            if (!ExtraChildren.Contains(element))
                                ExtraChildren.Add(element);
                        }
                    }
                    //System.Diagnostics.Debug.WriteLine($"AlignedPlacementPanel left: left[{leftWidth}] center[{centerWidth}] right[{rightWidth}]");
                }
                // System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange i[" + i + "] RIGHT");
                if (i<rightChildren.Count)
                {
                    var element = rightChildren[i];
                    //System.Diagnostics.Debug.WriteLine($"AlignedPlacementPanel right: left[{leftWidth}] center[{centerWidth}] right[{rightWidth}]");
                    if (!rightDone && leftWidth + centerWidth + rightWidth +  element.DesiredSize.Width < finalSize.Width)
                    {
                        if (ExtraChildren.Contains(element))
                            ExtraChildren.Remove(element);

                        if (i > 0) // only add spacing to right if there is another right element
                            rightWidth += Spacing;
                        rightWidth += element.DesiredSize.Width;
                        //System.Diagnostics.Debug.Write("AlignedPlacementPanel.Arrange right[" + i + "] ...");
                        element.Arrange(new Rect(new Point(finalSize.Width-rightWidth, (finalSize.Height - element.DesiredSize.Height) / 2.0), element.DesiredSize));
                        //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel right: finalSize.Width-rightWidth: " + (finalSize.Width - rightWidth));
                        //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel right: element.DesiredSize: " + element.DesiredSize);
                        //System.Diagnostics.Debug.WriteLine(" ! ");
                    }
                    else
                    {
                        rightDone = true;
                        while (rightChildren.Count > i)
                        {
                            element = rightChildren[i];
                            rightChildren.Remove(element);
                            if (!ExtraChildren.Contains(element))
                                ExtraChildren.Add(element);
                        }
                    }
                    //System.Diagnostics.Debug.WriteLine($"AlignedPlacementPanel right: left[{leftWidth}] center[{centerWidth}] right[{rightWidth}]");
                }
                //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange i[" + i + "] CENTER centerChildren.Count["+centerChildren.Count+"]");
                if (i<centerChildren.Count)
                {
                    var element = centerChildren[i];
                    //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange center[" + i + "]["+element+"] size:[" + element.DesiredSize + "] centerDone: "+ centerDone);
                    //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange leftWidth["+leftWidth+"] centerWidth["+centerWidth+"] rightWidth["+rightWidth+"] element.Des.W["+element.DesiredSize.Width+"] finalSize.W["+finalSize.Width+"]");
                    //System.Diagnostics.Debug.WriteLine($"AlignedPlacementPanel center: left[{leftWidth}] center[{centerWidth}] right[{rightWidth}]");
                    if (!centerDone && leftWidth + centerWidth + rightWidth + element.DesiredSize.Width < finalSize.Width)
                    {
                        if (ExtraChildren.Contains(element))
                            ExtraChildren.Remove(element);
                        //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange KEEP");
                        centerWidth += element.DesiredSize.Width;
                        centerWidth += Spacing; // like LEFT, always add spacing to CENTER
                    }
                    else
                    {
                        centerDone = true;
                        while (centerChildren.Count > i)
                        {
                            element = centerChildren[i];
                            //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange REMOVE ["+element+"]");
                            centerChildren.Remove(element);
                            if (!ExtraChildren.Contains(element))
                                ExtraChildren.Add(element);
                        }
                    }
                    //System.Diagnostics.Debug.WriteLine($"AlignedPlacementPanel center: left[{leftWidth}] center[{centerWidth}] right[{rightWidth}]");
                }
            }

            var gap = finalSize.Width - rightWidth - centerWidth - leftWidth;
            //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.ArrangeOverride: gap: " + gap);
            var centerX = leftWidth + gap / 2;
            //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.ArrangeOverride: centerX: " + centerX);
            if (!centerChildren.Contains(_moreButton))
                _moreButton.Arrange(new Rect(new Point(finalSize.Width + 10, finalSize.Height +10), _moreButton.DesiredSize));
            foreach (var element in centerChildren)
            {
                //System.Diagnostics.Debug.Write("AlignedPlacementPanel.Arrange center[" + element + "] ...");
                element.Arrange(new Rect(new Point(centerX, (finalSize.Height - element.DesiredSize.Height) / 2.0), element.DesiredSize));
                //System.Diagnostics.Debug.WriteLine(" ! ");
                centerX += element.DesiredSize.Width + Spacing;
            }
            //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.ArrangeOverride: centerX: " + centerX);


            foreach (var child in ExtraChildren)
            {
                child.Arrange(new Rect(new Point(finalSize.Width, 0), child.DesiredSize));
                //if (Children.Contains(child))
                //Children.Remove(child);
            }

            _arranging = false;
            //System.Diagnostics.Debug.WriteLine("AlignedPlacementPanel.Arrange EXIT");
            return finalSize;
        }
    }
}
