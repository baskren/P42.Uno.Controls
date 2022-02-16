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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;

namespace P42.Uno.Controls
{
    [Windows.UI.Xaml.Data.Bindable]
    public partial class NewSegmentedControl : Grid
    {
        #region Properties

        #region Padding Property
        public static new readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(NewSegmentedControl),
            new PropertyMetadata(new Thickness(4))
        );
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
            typeof(NewSegmentedControl),
            new PropertyMetadata(default(IList<string>), OnLabelsChanged)
        );
        private static void OnLabelsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is NewSegmentedControl control)
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
            typeof(NewSegmentedControl),
            new PropertyMetadata(1.0, OnBorderWidthPropertyChanged)
        );

        private static void OnBorderWidthPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is NewSegmentedControl sc)
                sc.UpdateBorder();
        }

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
            typeof(NewSegmentedControl),
            new PropertyMetadata(-1, OnSelectedIndexChanged)
            );

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is NewSegmentedControl control && control._tapProcessing == int.MinValue)
            {
                control.SelectionTracker.SelectIndex((int)args.NewValue);
            }
        }

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
            typeof(NewSegmentedControl),
            new PropertyMetadata(null, OnSelectedLabelChanged)
            );

        private static void OnSelectedLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is NewSegmentedControl control && control._tapProcessing == int.MinValue)
            {
                control.SelectionTracker.SelectItem((string)args.NewValue);
            }
        }

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
            typeof(NewSegmentedControl),
            new PropertyMetadata(false, OnAllowUnselectAllChanged)
            );

        private static void OnAllowUnselectAllChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is NewSegmentedControl control)
                control.SelectionTracker.AllowUnselectAll = (bool)args.NewValue;
        }

        public bool AllowUnselectAll
        {
            get => (bool)GetValue(AllowUnselectAllProperty);
            set => SetValue(AllowUnselectAllProperty, value);
        }
        #endregion

        #region SelectedIndexes
        public List<int> SelectedIndexes
        {
            get => SelectionTracker.SelectedIndexes.Where(i => i < Labels.Count).ToList();
            set => SelectionTracker.SelectedIndexes = value;
        }
        #endregion

        #region SelectedItems
        public List<string> SelectedItems
        {
            get => SelectionTracker.SelectedItems;
            set => SelectionTracker.SelectedItems = value;
        }
        #endregion

        #region SelectionMode Property
        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(
            nameof(SelectionMode),
            typeof(SelectionMode),
            typeof(NewSegmentedControl),
            new PropertyMetadata(default, OnSelectionModelChanged)
            );

        private static void OnSelectionModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is NewSegmentedControl control)
            {
                control.SelectionTracker.SelectionMode = (SelectionMode)args.NewValue;
            }
        }

        public SelectionMode SelectionMode
        {
            get => (SelectionMode)GetValue(SelectionModeProperty);
            set => SetValue(SelectionModeProperty, value);
        }
        #endregion

        #region IsOverflowed
        bool _isOverflowed;
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
        public event EventHandler<bool> IsOverflowedChanged;
        public event EventHandler<(int SelectedIndex, string SelectedLabel)> SelectionChanged;
        #endregion


        #region Fields
        readonly List<TextBlock> TextBlocks = new List<TextBlock>();
        readonly List<Rectangle> Separators = new List<Rectangle>();
        readonly List<Rectangle> Backgrounds = new List<Rectangle>();
        readonly CollectionSelectionTracker<string> SelectionTracker = new CollectionSelectionTracker<string>();
        readonly TextBlock _testTextBlock = new TextBlock().FontSize(16);
        #endregion


        #region Constructor
        public NewSegmentedControl()
        {
            _testTextBlock
                .Bind(TextBlock.MarginProperty, this, nameof(Padding));

            SizeChanged += NewSegmentedControl_SizeChanged;
            BorderBrush = SystemToggleButtonBrushes.Border;
            CornerRadius = new CornerRadius(4);
            VerticalAlignment = VerticalAlignment.Center;

            Loaded += OnLoaded;

            Labels = new ObservableCollection<string>();

            SelectionTracker.CollectionChanged += OnSelectionTracker_CollectionChanged;
            SelectionTracker.SelectionChanged += OnSelectionTracker_SelectionChanged;

#if WINDOWS_UWP
            RegisterPropertyChangedCallback(Grid.BorderThicknessProperty, OnBorderThicknessChanged);
#endif
        }
        #endregion


        #region Gesture Handlers
        void OnSegmentTapped(object sender, TappedRoutedEventArgs e)
        {
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
            if (sender is FrameworkElement d)
            {
                var index = (int)d.GetValue(Grid.ColumnProperty);
                SetHoverOverColors(index);
            }
        }

        void OnSegmentPointerExited(object sender, PointerRoutedEventArgs e)
        {
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
                    ? BorderBrush.AssureGesturable() 
                    : SystemToggleButtonBrushes.Background.AssureGesturable();
                var textBlock = TextBlocks[index];
                textBlock.Foreground = selected
                    ? SystemToggleButtonBrushes.CheckedForeground
                    : SystemToggleButtonBrushes.Foreground;
                if (index < Separators.Count)
                {
                    var separator = Separators[index];
                    var nextSelected = SelectedIndexes.Contains(index + 1);
                    separator.Visible(selected == nextSelected);
                    separator.Fill = selected
                        ? SystemToggleButtonBrushes.CheckedForeground
                        : BorderBrush;
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

        private void NewSegmentedControl_SizeChanged(object sender, SizeChangedEventArgs args)
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
                .AddOnTapped(OnSegmentTapped)
                .AddOnPointerEntered(OnSegmentPointerEntered)
                .AddOnPointerExited(OnSegmentPointerExited)
                .AddOnPointerCanceled(OnSegmentPointerExited)
                .AddOnPointerCaptureLost(OnSegmentPointerExited)
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
                .AddOnTapped(OnSegmentTapped)
                .AddOnPointerEntered(OnSegmentPointerEntered)
                .AddOnPointerExited(OnSegmentPointerExited)
                .AddOnPointerCanceled(OnSegmentPointerExited)
                .AddOnPointerCaptureLost(OnSegmentPointerExited)
                // TODO: Add Tap Handler
                );

        void AddNewColumn()
            => ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        void RemoveLastColumn()
            => ColumnDefinitions.RemoveAt(ColumnDefinitions.Count - 1);
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

                System.Diagnostics.Debug.WriteLine($"cellWidth:{cellWidth} desiredWidth:{_testTextBlock.DesiredSize.Width} Padding.Hz:{Padding.Horizontal()}");

                if (_testTextBlock.DesiredSize.Width >= cellWidth)
                    return true;
            }
            return false;
        }

        void UpdateChildren()
        {
            var columns = Labels.Count;

            while (columns > ColumnDefinitions.Count)
                AddNewColumn();
            while (columns < ColumnDefinitions.Count)
                RemoveLastColumn();

            if (IsOverflowed)
                Children.Clear();
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

                    if (!Children.Contains(TextBlocks[i]))
                        Children.Add(TextBlocks[i]);
                    if (i < columns - 1 && !Children.Contains(Separators[i]))
                        Children.Add(Separators[i]);
                    if (!Children.Contains(Backgrounds[i]))
                        Children.Insert(0, Backgrounds[i]);
                }

                var remove = Children.Where(c => (int)c.GetValue(Grid.ColumnProperty) >= columns);
                Children.RemoveRange(remove);

                remove = Children.Where(c => (int)c.GetValue(Grid.ColumnSpanProperty) > 1 && (int)c.GetValue(Grid.ColumnProperty) >= columns - 1);
                Children.RemoveRange(remove);

                DisplaySelections();
            }

            UpdateBorder();
        }

#if WINDOWS_UWP
        private void OnBorderThicknessChanged(DependencyObject sender, DependencyProperty dp)
        {
            if (IsLoaded && ActualWidth > 50)
                CalculateOverflow(ActualWidth);
        }

#else
        protected override void OnBorderThicknessChanged(Thickness oldValue, Thickness newValue)
        {
            base.OnBorderThicknessChanged(oldValue, newValue);
            if (IsLoaded && ActualWidth > 50)
                CalculateOverflow(ActualWidth);
        }
#endif

        #endregion
    }


    
}


