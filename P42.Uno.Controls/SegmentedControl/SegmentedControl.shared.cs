using P42.Uno.Markup;
using P42.Utils.Uno;
using P42.Utils;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
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
            if (dependencyObject is SegmentedControl control)
            {
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
            if (d is SegmentedControl control && control._tapProcessing == int.MinValue)
            {
                control.SelectionTracker.SelectIndex((int)args.NewValue);
            }
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
            if (d is SegmentedControl control && control._tapProcessing == int.MinValue)
            {
                control.SelectionTracker.SelectItem((string)args.NewValue);
            }
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
            {
                control.SelectionTracker.SelectionMode = (SelectionMode)args.NewValue;
            }
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

        #region IsOverflowed
        bool _isOverflowed;
        /// <summary>
        /// Does the segment's label exceed the available space?
        /// </summary>
        public bool IsOverflowed
        {
            get => _isOverflowed;
            private set
            {
                if (_isOverflowed != value)
                {
                    _isOverflowed = value;
                    UpdateBorder();
                    IsOverflowedChanged?.Invoke(this, value);
                }
            }
        }
        #endregion

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
                .Bind(TextBlock.MarginProperty, this, nameof(Padding));

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

            grid.Bind(Grid.CornerRadiusProperty, this, nameof(CornerRadius));
            grid.Bind(Grid.BorderBrushProperty, this, nameof(BorderBrush));
            grid.Bind(Grid.BorderThicknessProperty, this, nameof(BorderThickness));
        }

        #endregion


        #region Programmatic Selection
        public SegmentedControl SelectLabel(string label)
        {
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
        }

        public SegmentedControl DeselectLabel(string label)
        {
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
        }

        public SegmentedControl SelectIndex(int index)
        {
            if (index < 0 || index >= Labels.Count) return this;

            SelectLabel(Labels[index]);

            return this;
        }

        public SegmentedControl DeselectIndex(int index)
        {
            if (index < 0 || index >= Labels.Count) return this;

            DeselectLabel(Labels[index]);

            return this;
        }

        public SegmentedControl SelectAll()
        {
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

            return this;
        }

        public SegmentedControl DeselectAll()
        {
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

            return this;
        }
        #endregion


        #region Gesture Handlers
        private void OnIsEnabledChanged(DependencyObject sender, DependencyProperty dp)
        {
            if (IsLoaded)
            {
                for (int i = 0; i < Labels.Count; i++)
                    SetDefaultElementColors(i);
            }
        }

        void OnSegmentTapped(object sender, TappedRoutedEventArgs e)
        {
            if (!IsEnabled)
                return;

            if (sender is FrameworkElement d)
            {
                var index = (int)d.GetValue(Grid.ColumnProperty);
                SetTappedProcessingColors(index);
                if (SelectionTracker.SelectedIndexes.Contains(index))
                    SelectionTracker.UnselectIndex(index);
                else
                    SelectionTracker.SelectIndex(index);
            }
        }

        void OnSegmentPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (!IsEnabled) 
                return; 

            if (sender is FrameworkElement d)
            {
                var index = (int)d.GetValue(Grid.ColumnProperty);
                SetHoverOverColors(index);
            }
        }

        void OnSegmentPointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (!IsEnabled) return;

            if (sender is FrameworkElement d)
            {
                var index = (int)d.GetValue(Grid.ColumnProperty);
                SetDefaultElementColors(index);
            }
        }

        void SetDefaultElementColors(int index)
        {
            if (index >= 0 && index < Labels.Count && index < TextBlocks.Count && index < Backgrounds.Count)
            {
                var selected = SelectedIndexes.Contains(index);

                if (_tapProcessing == index)
                    return;

                var background = Backgrounds[index];
                background.Fill = SelectedIndexes.Contains(index)
                    ? BorderBrush.AsGesterableEnabled(IsEnabled) 
                    : SystemToggleButtonBrushes.Background.AsGesterableEnabled(IsEnabled);
                var textBlock = TextBlocks[index];
                textBlock.Foreground = selected
                    ? SystemToggleButtonBrushes.CheckedForeground.AsGesterableEnabled(IsEnabled)
                    : SystemToggleButtonBrushes.Foreground.AsGesterableEnabled(IsEnabled);
                if (index < Separators.Count)
                {
                    var separator = Separators[index];
                    var nextSelected = SelectedIndexes.Contains(index + 1);
                    separator.Visible(selected == nextSelected);
                    separator.Fill = selected
                        ? SystemToggleButtonBrushes.CheckedForeground.AsGesterableEnabled(IsEnabled)
                        : BorderBrush.AsGesterableEnabled(IsEnabled);
                }
            }
        }

        void SetTappedProcessingColors(int index)
        {
            if (index >= 0 && index < TextBlocks.Count)
            {
                var background = Backgrounds[index];
                background.Fill = SystemToggleButtonBrushes.BackgroundCheckedPressed;
                var textBlock = TextBlocks[index];
                textBlock.Foreground = SystemToggleButtonBrushes.ForegroundCheckedPressed;
            }
        }

        void SetHoverOverColors(int index)
        {
            if (index >= 0 && index < TextBlocks.Count)
            {
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
        }
        #endregion


        #region Change Handlers
        int _tapProcessing = int.MinValue;
        void OnSelectionTracker_SelectionChanged(object sender, CollectionSelectionTrackerSelectionChangedArguments<string> e)
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

        void OnSelectionTracker_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_tapProcessing != int.MinValue)
                DisplaySelections();
        }

        void DisplaySelections()
        {
            if (IsLoaded)
            {
                for (int i=0; i< Labels.Count;i++)
                    SetDefaultElementColors(i);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (ActualWidth > 20)
            {
                IsOverflowed = CalculateOverflow(ActualSize.X);
                UpdateChildren();
            }
        }

        private void SegmentedControl_SizeChanged(object sender, SizeChangedEventArgs args)
        {
            if (IsLoaded)
            {
                IsOverflowed = CalculateOverflow(ActualSize.X);
                UpdateChildren();
            }
        }

        private void Labels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                IsOverflowed = CalculateOverflow(ActualSize.X);
                UpdateChildren();
            }
        }
        #endregion


        #region Convenience Methods
        void UpdateBorder()
        {
            if (IsOverflowed || !Labels.Any())
                this.BorderThickness(0);
            else
                this.BorderThickness(BorderWidth);
        }

        void AddNewLabel()
            => TextBlocks.Add(new TextBlock()
                .Bind(TextBlock.MarginProperty, this, nameof(Padding))
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

        void AddNewSeparator()
            => Separators.Add(new Rectangle()
                .Margin(0,5)
                .Opacity(0.5)
                .Width(1)
                .StretchVertical().CenterHorizontal()
                .ColumnSpan(2).Column(Separators.Count)
                .Bind(Rectangle.FillProperty, this, nameof(BorderBrush))
                );

        void AddNewBackground()
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

        void AddNewColumn()
            => grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        void RemoveLastColumn()
            => grid.ColumnDefinitions.RemoveAt(grid.ColumnDefinitions.Count - 1);
        #endregion


        #region Layout
        Size big = new Size(5000, 5000);
        bool CalculateOverflow(double availableWidth)
        {
            if (!Labels.Any())
                return true;

            var columns = Labels.Count;
            double cellWidth = (availableWidth - 2 * BorderWidth - Margin.Horizontal())/(Math.Max(1, columns)) - BorderWidth - Padding.Horizontal();

            for (int i=0; i<columns;i++)
            {
                _testTextBlock.Text(Labels[i]).Measure(big);

                //System.Diagnostics.Debug.WriteLine($"cellWidth:{cellWidth} desiredWidth:{_testTextBlock.DesiredSize.Width} Padding.Hz:{Padding.Horizontal()}");

                if (_testTextBlock.DesiredSize.Width >= cellWidth)
                    return true;
            }
            return false;
        }

        void UpdateChildren()
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

                for (int i = 0; i < Labels.Count; i++)
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


