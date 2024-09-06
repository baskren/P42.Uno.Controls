using P42.Uno.Markup;
using P42.Utils.Uno;
using P42.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Windows.Foundation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Shapes;

namespace P42.Uno.Controls
{
    [Microsoft.UI.Xaml.Data.Bindable]
    public partial class SegmentedControl : UserControl
    {
        #region Properties

        #region Padding Property
        public static new readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(SegmentedControl),
            new PropertyMetadata(new Thickness(4))
        );
        /// <summary>
        /// How much padding to apply between segment and it's label?
        /// </summary>
        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        #endregion

        #region Labels Property
        public static readonly DependencyProperty LabelsProperty = DependencyProperty.Register(
            nameof(Labels),
            typeof(IList<string>),
            typeof(SegmentedControl),
            new PropertyMetadata(default(IList<string>), OnLabelsChanged)
        );
        private static void OnLabelsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not SegmentedControl control)
                return;

            if (args.NewValue is IList<string> newList)
            {
                if (args.OldValue is ObservableCollection<string> oldCollection)
                    oldCollection.CollectionChanged -= control.Labels_CollectionChanged;
                control.SelectionTracker.Collection = newList;
                if (args.NewValue is ObservableCollection<string> newCollection)
                    newCollection.CollectionChanged += control.Labels_CollectionChanged;
                control.Labels_CollectionChanged(null, null);
            }
            else
            {
                control.SelectionTracker.Collection = null;
                control.Labels.Clear();
            }

            control.UpdateChildren();
        }
        /// <summary>
        /// Segment labels
        /// </summary>
        public IList<string> Labels
        {
            get => (IList<string>)GetValue(LabelsProperty);
            set => SetValue(LabelsProperty, value);
        }
        #endregion Segments Property

        #region BorderWidth
        public static readonly DependencyProperty BorderWidthProperty = DependencyProperty.Register(
            nameof(BorderWidth),
            typeof(double),
            typeof(SegmentedControl),
            new PropertyMetadata(1.0, OnBorderWidthPropertyChanged)
        );

        private static void OnBorderWidthPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is SegmentedControl sc)
                sc.UpdateBorder();
        }
        /// <summary>
        /// Width of border
        /// </summary>
        public double BorderWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }
        #endregion

        #region SelectedIndex Property
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
            nameof(SelectedIndex),
            typeof(int),
            typeof(SegmentedControl),
            new PropertyMetadata(-1, OnSelectedIndexChanged)
            );

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is SegmentedControl { _tapProcessing: int.MinValue } control)
                control.SelectionTracker.SelectIndex((int)args.NewValue);
        }
        /// <summary>
        /// Index of selected segment
        /// </summary>
        public int SelectedIndex
        {
            get => Math.Min((int)GetValue(SelectedIndexProperty), Labels.Count-1);
            set => SetValue(SelectedIndexProperty, value);
        }
        #endregion

        #region Selected Label Property
        public static readonly DependencyProperty SelectedLabelProperty = DependencyProperty.Register(
            nameof(SelectedLabel),
            typeof(string),
            typeof(SegmentedControl),
            new PropertyMetadata(null, OnSelectedLabelChanged)
            );

        private static void OnSelectedLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is SegmentedControl { _tapProcessing: int.MinValue } control)
                control.SelectionTracker.SelectItem((string)args.NewValue);
        }
        /// <summary>
        /// Label text of selected segment
        /// </summary>
        public string SelectedLabel
        {
            get => (string)GetValue(SelectedLabelProperty);
            set => SetValue(SelectedLabelProperty, value);
        }
        #endregion

        #region AllowUnselectAll Property
        public static readonly DependencyProperty AllowUnselectAllProperty = DependencyProperty.Register(
            nameof(AllowUnselectAll),
            typeof(bool),
            typeof(SegmentedControl),
            new PropertyMetadata(false, OnAllowUnselectAllChanged)
            );

        private static void OnAllowUnselectAllChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is SegmentedControl control)
                control.SelectionTracker.AllowUnselectAll = (bool)args.NewValue;
        }
        /// <summary>
        /// Can we unselect all or not?
        /// </summary>
        public bool AllowUnselectAll
        {
            get => (bool)GetValue(AllowUnselectAllProperty);
            set => SetValue(AllowUnselectAllProperty, value);
        }
        #endregion

        #region SelectedIndexes
        /// <summary>
        /// List of selected indexes
        /// </summary>
        public List<int> SelectedIndexes
        {
            get => SelectionTracker.SelectedIndexes.Where(i => i < Labels.Count).ToList();
            set => SelectionTracker.SelectedIndexes = value;
        }
        #endregion

        #region SelectedItems
        /// <summary>
        /// List of selected labels
        /// </summary>
        public List<string> SelectedLabels
        {
            get => SelectionTracker.SelectedItems;
            set => SelectionTracker.SelectedItems = value;
        }
        #endregion

        #region SelectionMode Property
        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(
            nameof(SelectionMode),
            typeof(SelectionMode),
            typeof(SegmentedControl),
            new PropertyMetadata(default, OnSelectionModelChanged)
            );

        private static void OnSelectionModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is SegmentedControl control)
                control.SelectionTracker.SelectionMode = (SelectionMode)args.NewValue;
        }
        /// <summary>
        /// Selection Mode?
        /// </summary>
        public SelectionMode SelectionMode
        {
            get => (SelectionMode)GetValue(SelectionModeProperty);
            set => SetValue(SelectionModeProperty, value);
        }
        #endregion


        #region IsOverflowed Property
        public static readonly DependencyProperty IsOverflowedProperty = DependencyProperty.Register(
            nameof(IsOverflowed),
            typeof(bool),
            typeof(SegmentedControl),
            new PropertyMetadata(default(bool), (d,e) => ((SegmentedControl)d).OnIsOverflowedChanged(e))
        );

        private void OnIsOverflowedChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateBorder();
            IsOverflowedChanged?.Invoke(this, IsOverflowed);
        }

        public bool IsOverflowed
        {
            get => (bool)GetValue(IsOverflowedProperty);
            set => SetValue(IsOverflowedProperty, value);
        }
        #endregion IsOverflowed Property

        #endregion


        #region Events
        /// <summary>
        /// Has there been a change to if there is any labels greater than the available space?
        /// </summary>
        public event EventHandler<bool> IsOverflowedChanged;
        /// <summary>
        /// Has there been a selection change?
        /// </summary>
        public event EventHandler<(int SelectedIndex, string SelectedLabel)> SelectionChanged;
        #endregion


        #region Fields
        readonly List<TextBlock> TextBlocks = new List<TextBlock>();
        readonly List<Rectangle> Separators = new List<Rectangle>();
        readonly List<Rectangle> Backgrounds = new List<Rectangle>();
        readonly CollectionSelectionTracker<string> SelectionTracker = new CollectionSelectionTracker<string>();
        readonly TextBlock _testTextBlock = new TextBlock().FontSize(16);
        readonly Grid grid = new Grid();
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SegmentedControl()
        {
            Content = grid;

            _testTextBlock
                .WBind(TextBlock.MarginProperty, this, PaddingProperty);

            SizeChanged += SegmentedControl_SizeChanged;
            BorderBrush = SystemToggleButtonBrushes.Border;
            CornerRadius = new CornerRadius(4);
            VerticalAlignment = VerticalAlignment.Center;

            Loaded += OnLoaded;

            Labels = new ObservableCollection<string>();

            SelectionTracker.CollectionChanged += OnSelectionTracker_CollectionChanged;
            SelectionTracker.SelectionChanged += OnSelectionTracker_SelectionChanged;

#if !HAS_UNO
            RegisterPropertyChangedCallback(UserControl.BorderThicknessProperty, OnBorderThicknessChanged);
#endif
            RegisterPropertyChangedCallback(UserControl.IsEnabledProperty, OnIsEnabledChanged);

            grid.WBind(Grid.CornerRadiusProperty, this, CornerRadiusProperty);
            grid.WBind(Grid.BorderBrushProperty, this, BorderBrushProperty);
            grid.WBind(Grid.BorderThicknessProperty, this, BorderThicknessProperty);
        }

        #endregion


        #region Programmatic Selection
        public SegmentedControl SelectLabel(string label)
        {
            return SelectIndex(Labels.IndexOf(label));
            /*
            if (!Labels.Contains(label))
                return this;

            foreach(var child in grid.Children)
            {
                if (child is not TextBlock textBlock)
                    continue;

                if (textBlock.Text == label && !SelectedLabels.Contains(label))
                {
                    OnSegmentTapped(label, new TappedRoutedEventArgs());
                    return this;
                }
            }
            return this;
            */
        }

        public SegmentedControl DeselectLabel(string label)
        {
            return DeselectIndex(Labels.IndexOf(label));
            /*
            if (!Labels.Contains(label))
                return this;

            foreach (var child in grid.Children)
            {
                if (child is not TextBlock textBlock)
                    continue;

                if (textBlock.Text == label && SelectedLabels.Contains(label))
                {
                    OnSegmentTapped(label, new TappedRoutedEventArgs());
                    return this;
                }
            }
            return this;
            */

        }

        public SegmentedControl SelectIndex(int index)
        {
            if (index < 0 && SelectionMode == SelectionMode.Radio)
            {
                SelectionTracker.Clear();
                return this;
            }
            
            if (index < 0 || index >= Labels.Count) return this;

            //SelectLabel(Labels[index]);
            if (!SelectionTracker.SelectedIndexes.Contains(index))
                SelectionTracker.SelectIndex(index);

            return this;
        }

        public SegmentedControl DeselectIndex(int index)
        {
            if (index < 0 || index >= Labels.Count) return this;

            //DeselectLabel(Labels[index]);
            if (SelectionTracker.SelectedIndexes.Contains(index))
                SelectionTracker.UnselectIndex(index);

            return this;
        }

        public SegmentedControl SelectAll()
        {
            /*
            foreach (var child in grid.Children)
            {
                if (child is not TextBlock textBlock)
                    continue;

                if (!SelectedLabels.Contains(textBlock.Text))
                {
                    OnSegmentTapped(textBlock.Text, new TappedRoutedEventArgs());
                    return this;
                }
            }
            */
            for (var i = 0; i < Labels.Count; i++)
                SelectIndex(i);

            return this;
        }

        public SegmentedControl DeselectAll()
        {
            /*
            foreach (var child in grid.Children)
            {
                if (child is not TextBlock textBlock)
                    continue;

                if (SelectedLabels.Contains(textBlock.Text))
                {
                    OnSegmentTapped(textBlock.Text, new TappedRoutedEventArgs());
                    return this;
                }
            }
            */

            for (var i = 0; i < Labels.Count; i++)
                DeselectIndex(i);

            return this;
        }
        #endregion


        #region Gesture Handlers
        private void OnIsEnabledChanged(DependencyObject sender, DependencyProperty dp)
        {
            if (!IsLoaded)
                return;

            for (var i = 0; i < Labels.Count; i++)
                UpdateElementColors(i);
        }

        private void OnSegmentTapped(object sender, TappedRoutedEventArgs e)
        {
            if (!IsEnabled)
                return;

            if (sender is not FrameworkElement d)
                return;

            var index = (int)d.GetValue(Grid.ColumnProperty);
            SetTappedProcessingColors(index);
            if (SelectionTracker.SelectedIndexes.Contains(index))
                SelectionTracker.UnselectIndex(index);
            else
                SelectionTracker.SelectIndex(index);
        }


        private void OnSegmentPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (!IsEnabled) 
                return;

            if (sender is not FrameworkElement d)
                return;

            var index = (int)d.GetValue(Grid.ColumnProperty);
            SetHoverOverColors(index);
        }

        private void OnSegmentPointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (!IsEnabled) return;

            if (sender is not FrameworkElement d)
                return;

            var index = (int)d.GetValue(Grid.ColumnProperty);
            UpdateElementColors(index);
        }

        private void UpdateElementColors(int index)
        {
            if (index < 0 || index >= Labels.Count || index >= TextBlocks.Count || index >= Backgrounds.Count)
                return;

            if (_tapProcessing == index)
                return;

            var selected = SelectedIndexes.Contains(index);
            var nextSelected = SelectedIndexes.Contains(index + 1);

            var background = Backgrounds[index];
            background.Fill = selected
                ? BorderBrush.AsGesterableEnabled(IsEnabled) 
                : SystemToggleButtonBrushes.Background.AsGesterableEnabled(IsEnabled);

            var textBlock = TextBlocks[index];
            textBlock.Foreground = selected
                ? SystemToggleButtonBrushes.CheckedForeground.AsGesterableEnabled(IsEnabled)
                : Foreground; // SystemToggleButtonBrushes.Foreground.AsGesterableEnabled(IsEnabled);

            if (index >= Separators.Count)
                return;

            var separator = Separators[index];
            separator.Visible(selected == nextSelected);
            separator.Fill = selected
                ? SystemToggleButtonBrushes.CheckedForeground.AsGesterableEnabled(IsEnabled)
                : BorderBrush.AsGesterableEnabled(IsEnabled);
        }

        private void SetTappedProcessingColors(int index)
        {
            if (index < 0 || index >= TextBlocks.Count)
                return;

            var background = Backgrounds[index];
            background.Fill = SystemToggleButtonBrushes.BackgroundCheckedPressed;
            var textBlock = TextBlocks[index];
            textBlock.Foreground = SystemToggleButtonBrushes.ForegroundCheckedPressed;
        }

        private void SetHoverOverColors(int index)
        {
            if (index < 0 || index >= TextBlocks.Count)
                return;
 
            var selected = SelectedIndexes.Contains(index);
            if (_tapProcessing == index)
                return;
            var background = Backgrounds[index];
            background.Fill = selected
                ? SystemToggleButtonBrushes.CheckedPointerOverBackground.AssureGesturable()
                : SystemButtonBrushes.BackgroundPointerOver.AssureGesturable();
            var textBlock = TextBlocks[index];
            textBlock.Foreground = selected
                ? SystemToggleButtonBrushes.ForegroundCheckedPointerOver
                : SystemButtonBrushes.ForegroundPointerOver;
        }
        #endregion


        #region Change Handlers
        private int _tapProcessing = int.MinValue;
        private void OnSelectionTracker_SelectionChanged(object sender, CollectionSelectionTrackerSelectionChangedArguments<string> e)
        {
            // show processing position
            SetTappedProcessingColors(e.NewIndex);

            // fire events
            _tapProcessing = SelectionTracker.SelectedIndex;
            SetValue(SelectedIndexProperty, e.NewIndex);
            SetValue(SelectedLabelProperty, e.NewItem);
            SelectionChanged?.Invoke(this, (e.NewIndex, e.NewItem));

            _tapProcessing = int.MinValue;
            DisplaySelections();
        }

        private void OnSelectionTracker_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_tapProcessing != int.MinValue)
                DisplaySelections();
        }

        private void DisplaySelections()
        {
            if (!IsLoaded)
                return;

            for (var i=0; i< Labels.Count;i++)
                UpdateElementColors(i);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!(ActualWidth > 20))
                return;

            IsOverflowed = CalculateOverflow(ActualSize.X);
            UpdateChildren();
        }

        private void SegmentedControl_SizeChanged(object sender, SizeChangedEventArgs args)
        {
            if (!IsLoaded)
                return;

            IsOverflowed = CalculateOverflow(ActualSize.X);
            UpdateChildren();
        }

        private void Labels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            IsOverflowed = CalculateOverflow(ActualSize.X);
            UpdateChildren();
        }
        #endregion


        #region Convenience Methods
        private void UpdateBorder()
        {
            if (IsOverflowed || !Labels.Any())
                this.BorderThickness(0);
            else
                this.BorderThickness(BorderWidth);
        }

        private void AddNewLabel()
            => TextBlocks.Add(new TextBlock()
                .WBind(TextBlock.MarginProperty, this, PaddingProperty)
                .Foreground(SystemToggleButtonBrushes.Foreground)
                .Center()
                .FontSize(16)
                .Column(TextBlocks.Count)
                .AddTappedHandler(OnSegmentTapped)
                .AddPointerEnteredHandler(OnSegmentPointerEntered)
                .AddPointerExitedHandler(OnSegmentPointerExited)
                .AddPointerCanceledHandler(OnSegmentPointerExited)
                .AddPointerCaptureLostHandler(OnSegmentPointerExited)
                );

        private void AddNewSeparator()
            => Separators.Add(new Rectangle()
                .Margin(0,5)
                .Opacity(0.5)
                .Width(1)
                .StretchVertical().CenterHorizontal()
                .ColumnSpan(2).Column(Separators.Count)
                .WBind(Shape.FillProperty, this, BorderBrushProperty)
                );

        private void AddNewBackground()
            => Backgrounds.Add(new Rectangle()
                .Fill(SystemToggleButtonBrushes.Background)
                .Margin(-1)
                .Stretch()
                .Column(Backgrounds.Count)
                .AddTappedHandler(OnSegmentTapped)
                .AddPointerEnteredHandler(OnSegmentPointerEntered)
                .AddPointerExitedHandler(OnSegmentPointerExited)
                .AddPointerCanceledHandler(OnSegmentPointerExited)
                .AddPointerCaptureLostHandler(OnSegmentPointerExited)
                // TODO: Add Tap Handler
                );

        private void AddNewColumn()
            => grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        private void RemoveLastColumn()
            => grid.ColumnDefinitions.RemoveAt(grid.ColumnDefinitions.Count - 1);
        #endregion


        #region Layout
        private readonly Size big = new Size(5000, 5000);
        private bool CalculateOverflow(double availableWidth)
        {
            if (!Labels.Any())
                return true;

            var columns = Labels.Count;
            var cellWidth = (availableWidth - 2 * BorderWidth - Margin.Horizontal())/(Math.Max(1, columns)) - BorderWidth - Padding.Horizontal();

            for (var i=0; i<columns;i++)
            {
                _testTextBlock.Text(Labels[i]).Measure(big);

                //System.Diagnostics.Debug.WriteLine($"cellWidth:{cellWidth} desiredWidth:{_testTextBlock.DesiredSize.Width} Padding.Hz:{Padding.Horizontal()}");

                if (_testTextBlock.DesiredSize.Width >= cellWidth)
                    return true;
            }
            return false;
        }

        private void UpdateChildren()
        {
            var columns = Labels.Count;

            while (columns > grid.ColumnDefinitions.Count)
                AddNewColumn();
            while (columns < grid.ColumnDefinitions.Count)
                RemoveLastColumn();

            if (IsOverflowed)
                grid.Children.Clear();
            else
            {
                while (TextBlocks.Count < columns)
                    AddNewLabel();
                while (Separators.Count < columns - 1)
                    AddNewSeparator();
                while (Backgrounds.Count < columns)
                    AddNewBackground();

                for (var i = 0; i < Labels.Count; i++)
                {
                    TextBlocks[i].Text(Labels[i]);

                    if (!grid.Children.Contains(TextBlocks[i]))
                        grid.Children.Add(TextBlocks[i]);
                    if (i < columns - 1 && !grid.Children.Contains(Separators[i]))
                        grid.Children.Add(Separators[i]);
                    if (!grid.Children.Contains(Backgrounds[i]))
                        grid.Children.Insert(0, Backgrounds[i]);
                }

                var remove = grid.Children.Where(c => (int)c.GetValue(Grid.ColumnProperty) >= columns);
                grid.Children.RemoveRange(remove);

                remove = grid.Children.Where(c => (int)c.GetValue(Grid.ColumnSpanProperty) > 1 && (int)c.GetValue(Grid.ColumnProperty) >= columns - 1);
                grid.Children.RemoveRange(remove);

                DisplaySelections();
            }

            UpdateBorder();
        }

        private void OnBorderThicknessChanged(DependencyObject sender, DependencyProperty dp)
        {
            if (IsLoaded && ActualWidth > 50)
                CalculateOverflow(ActualWidth);
        }


        #endregion
    }


    
}


