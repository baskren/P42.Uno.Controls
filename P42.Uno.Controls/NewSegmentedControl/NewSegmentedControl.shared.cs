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
    //[System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    public partial class NewSegmentedControl : Grid
    {
        #region Properties

        #region Padding Property
        public static new readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(NewSegmentedControl),
            new PropertyMetadata(default)
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
                    if (args.OldValue is ObservableCollection<string> newCollection)
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
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }
        #endregion

        #region Selected Item Property
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

        #region AllowUnselectLastSelected Property
        public static readonly DependencyProperty AllowUnselectLastSelectedProperty = DependencyProperty.Register(
            nameof(AllowUnselectLastSelected),
            typeof(bool),
            typeof(NewSegmentedControl),
            new PropertyMetadata(false, OnAllowUnselectLastSelectedChanged)
            );

        private static void OnAllowUnselectLastSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (d is NewSegmentedControl control)
                control.SelectionTracker.AllowUnselectLastSelected = (bool)args.NewValue;
        }

        public bool AllowUnselectLastSelected
        {
            get => (bool)GetValue(AllowUnselectLastSelectedProperty);
            set => SetValue(AllowUnselectLastSelectedProperty, value);
        }
        #endregion

        #region SelectedIndexes
        public List<int> SelectedIndexes
        {
            get => SelectionTracker.SelectedIndexes;
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
            set
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
        public event EventHandler<(int SelectedIndex, string SelectedItem)> SelectionChanged;
        #endregion


        #region Fields
        Thickness _hiddenBorderThickness;
        bool _hidingBorder;
        List<TextBlock> TextBlocks = new List<TextBlock>();
        List<Rectangle> Separators = new List<Rectangle>();
        List<Rectangle> Backgrounds = new List<Rectangle>();
        CollectionSelectionTracker<string> SelectionTracker = new CollectionSelectionTracker<string>();
        #endregion


        #region Constructor
        public NewSegmentedControl()
        {
            Padding = new Thickness(2, 4);

            SizeChanged += NewSegmentedControl_SizeChanged;
            BorderBrush = SystemToggleButtonBrushes.Border;
            CornerRadius = new CornerRadius(4);
            VerticalAlignment = VerticalAlignment.Center;

            Loaded += OnLoaded;

            Labels = new ObservableCollection<string>();

            //SelectionTracker.SelectionChanged += OnSelectionTrackerSelectionChanged;
            SelectionTracker.CollectionChanged += OnSelectionTracker_CollectionChanged;
            SelectionTracker.SelectionChanged += OnSelectionTracker_SelectionChanged;
        }

        #endregion


        #region Helper methods

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
            if (index >= 0 && index < TextBlocks.Count && index < Backgrounds.Count)
            {
                var selected = SelectedIndexes.Contains(index);
                if (_tapProcessing == index)
                    return;
                //System.Diagnostics.Debug.WriteLine($"DEFAULT [{index}]");
                var background = Backgrounds[index];
                background.Fill = SelectedIndexes.Contains(index)
                    ? BorderBrush.AssureGesturable() //SystemToggleButtonBrushes.CheckedBackground.AssureGesturable()
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
                    /*
                    separator.Margin = selectedPair
                        ? new Thickness(0, 3)
                        : new Thickness(0);
                    */
                }
            }
        }

        void SetTappedProcessingColors(int index)
        {
            if (index >= 0 && index < TextBlocks.Count)
            {
                //System.Diagnostics.Debug.WriteLine($"TAPPED [{index}]");
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
                //System.Diagnostics.Debug.WriteLine($"HOVER [{index}]");
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
            //await Task.Delay(50);

            // fire events
            _tapProcessing = SelectionTracker.SelectedIndex;
            SetValue(SelectedIndexProperty, e.NewIndex);
            SetValue(SelectedLabelProperty, e.NewItem);
            SelectionChanged?.Invoke(this, (e.NewIndex, e.NewItem));
            //await Task.Delay(50);

            // or fake it
            /*
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds < 1000)
                await Task.Delay(100);
            */
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
                //.Margin(Padding)
                .Bind(TextBlock.MarginProperty, this, nameof(Padding))
                .Foreground(SystemToggleButtonBrushes.Foreground)
                .Center()
                .GridColumn(TextBlocks.Count)
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
                .ColumnSpan(2).GridColumn(Separators.Count)
                .Bind(Rectangle.FillProperty, this, nameof(BorderBrush))
                );

        void AddNewBackground()
            => Backgrounds.Add(new Rectangle()
                .Fill(SystemToggleButtonBrushes.Background)
                .Margin(-0.25, 0)
                .Stretch()
                .GridColumn(Backgrounds.Count)
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
            double cellWidth = Padding.Left + Padding.Right + availableWidth/(Math.Max(1, columns)) + BorderWidth;

            while (TextBlocks.Count < columns)
                AddNewLabel();

            if (!IsOverflowed)
            {
                while (Separators.Count < columns - 1)
                    AddNewSeparator();
                while (Backgrounds.Count < columns)
                    AddNewBackground();
            }

            for (int i=0; i<columns;i++)
            {
                //TODO: Add IsSizeValid attached property to TextBlock as a way to eliminate unnecessary measures
                TextBlocks[i].Text(Labels[i]).Measure(big);
                if (TextBlocks[i].DesiredSize.Width >= cellWidth)
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

        protected override void OnBorderThicknessChanged(Thickness oldValue, Thickness newValue)
        {
            if (!_hidingBorder)
                _hiddenBorderThickness = newValue;
            base.OnBorderThicknessChanged(oldValue, newValue);
            if (IsLoaded && ActualWidth > 50)
                CalculateOverflow(ActualWidth);
        }
        #endregion
    }


    
}


