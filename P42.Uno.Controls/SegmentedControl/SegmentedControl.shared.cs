using P42.Uno.Markup;
using P42.Utils.Uno;
using P42.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
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

            // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{control._instance}] OnLabelsChanged : ENTER Labels=[{string.Join(",",control.Labels)}]  SELECTEDINDEXES=[{string.Join(",",control.SelectedIndexes)}] SelectedLabel=[{control.SelectedLabel}]");
            var beforeLabel = control.SelectedLabel;
            
            //control.SelectedIndex = -1;
            //control.SelectedLabel = null;
            //control._selectionTracker.Collection = null;
            
            // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{control._instance}] OnLabelsChanged : --A-- Labels=[{string.Join(",",control.Labels)}]  SELECTEDINDEXES=[{string.Join(",",control.SelectedIndexes)}] SelectedLabel=[{control.SelectedLabel}]");
            if (args.NewValue is IList<string> newList)
            {
                if (args.OldValue is ObservableCollection<string> oldCollection)
                    oldCollection.CollectionChanged -= control.Labels_CollectionChanged;
                control._selectionTracker.Collection = newList;

                // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{control._instance}] OnLabelsChanged : --B-- Labels=[{string.Join(",",control.Labels)}]  SELECTEDINDEXES=[{string.Join(",",control.SelectedIndexes)}] SelectedLabel=[{control.SelectedLabel}]");
                if (newList.Contains(beforeLabel))
                {
                    control.SelectedLabel = beforeLabel;
                    control.SelectedIndex = newList.IndexOf(beforeLabel);
                }

                // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{control._instance}] OnLabelsChanged : --C-- Labels=[{string.Join(",",control.Labels)}]  SELECTEDINDEXES=[{string.Join(",",control.SelectedIndexes)}] SelectedLabel=[{control.SelectedLabel}]");
                if (args.NewValue is ObservableCollection<string> newCollection)
                    newCollection.CollectionChanged += control.Labels_CollectionChanged;
                control.Labels_CollectionChanged(null, null);
            }
            else
            {
                // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{control._instance}] OnLabelsChanged : --D-- Labels=[{string.Join(",",control.Labels)}]  SELECTEDINDEXES=[{string.Join(",",control.SelectedIndexes)}] SelectedLabel=[{control.SelectedLabel}]");
                control._selectionTracker.Collection = null;
                control.SelectedIndex = -1;
            }
            
            // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{control._instance}] OnLabelsChanged : --E-- Labels=[{string.Join(",",control.Labels)}]  SELECTEDINDEXES=[{string.Join(",",control.SelectedIndexes)}] SelectedLabel=[{control.SelectedLabel}]");
            control.UpdateChildren();
            // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{control._instance}] OnLabelsChanged : EXIT Labels=[{string.Join(",",control.Labels)}]  SELECTEDINDEXES=[{string.Join(",",control.SelectedIndexes)}] SelectedLabel=[{control.SelectedLabel}]");
        }
        /// <summary>
        /// Segment labels
        /// </summary>
        public IList<string> Labels
        {
            get => (IList<string>)GetValue(LabelsProperty);
            set 
            {
                // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{_instance}] SETTING LABELS : BEFORE=[{string.Join(",",Labels ?? [])}] VALUE=[{string.Join(",",value)}]  SELECTEDINDEXES=[{string.Join(",",SelectedIndexes)}]");
                SetValue(LabelsProperty, value);
                // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{_instance}] SETTING LABELS : AFTER =[{string.Join(",",Labels)}]  SELECTEDINDEXES=[{string.Join(",",SelectedIndexes)}] ");
            }
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
            if (d is not SegmentedControl { _tapProcessing: int.MinValue } control)
                return;

            /*
            if (control._instance == 13)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"SegmentedControl[{control._instance}].OnSelectedIndexChanged SelectedIndex:{args.NewValue} : BEFORE : SelectedIndex=[{control.SelectedIndex}] SelectedLabel=[{control.SelectedLabel}] : _selectionTracker[{control._selectionTracker._instance}] .SelectedIndex[{control._selectionTracker.SelectedIndex}] .SelectedItem[{control._selectionTracker.SelectedItem}]");
                System.Diagnostics.Debug.WriteLine(
                    $"    SelectedIndexes:[{string.Join(',', control.SelectedIndexes)}] SelectedLabels:[{string.Join(',', control.SelectedLabels)}]");
            }
            */

            control._selectionTracker.SelectIndex((int)args.NewValue);
            /*
            control.DisplaySelections();
            if (control._instance == 13)
                System.Diagnostics.Debug.WriteLine($"SegmentedControl[{control._instance}].OnSelectedIndexChanged SelectedIndex:{args.NewValue} : AFTER  : SelectedIndex=[{control.SelectedIndex}] SelectedLabel=[{control.SelectedLabel}] : _selectionTracker[{control._selectionTracker._instance}] .SelectedIndex[{control._selectionTracker.SelectedIndex}] .SelectedItem[{control._selectionTracker.SelectedItem}]");
                */
        }
        /// <summary>
        /// Index of selected segment
        /// </summary>
        public int SelectedIndex
        {
            get => Math.Min((int)GetValue(SelectedIndexProperty), Labels.Count-1);
            set
            {
                // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{_instance}] SETTING SELECTED INDEX : BEFORE=[{SelectedIndex}] VALUE=[{value}]   SELECTEDINDEXES=[{string.Join(",",SelectedIndexes)}]");
                SetValue(SelectedIndexProperty, value);  
                // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{_instance}] SETTING SELECTED INDEX : AFTER =[{SelectedIndex}]  SELECTEDINDEXES=[{string.Join(",",SelectedIndexes)}]");
            } 
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
            {
                //System.Diagnostics.Debug.WriteLine($"SegmentedControl[{control._instance}].OnSelectedLabelChanged SelectedIndex:{args.NewValue} : BEFORE : SelectedIndex=[{control.SelectedIndex}] SelectedLabel=[{control.SelectedLabel}] : _selectionTracker[{control._selectionTracker._instance}] .SelectedIndex[{control._selectionTracker.SelectedIndex}] .SelectedItem[{control._selectionTracker.SelectedItem}]");
                control._selectionTracker.SelectItem((string)args.NewValue);
                //System.Diagnostics.Debug.WriteLine($"SegmentedControl[{control._instance}].OnSelectedLabelChanged SelectedIndex:{args.NewValue} : AFTER  : SelectedIndex=[{control.SelectedIndex}] SelectedLabel=[{control.SelectedLabel}] : _selectionTracker[{control._selectionTracker._instance}] .SelectedIndex[{control._selectionTracker.SelectedIndex}] .SelectedItem[{control._selectionTracker.SelectedItem}]");
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
                control._selectionTracker.AllowUnselectAll = (bool)args.NewValue;
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
            get => _selectionTracker.SelectedIndexes.Where(i => i < Labels.Count).ToList();
            set
            {
                // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{_instance}] SETTING SELECTED INDEXES : BEFORE=[{string.Join(",",_selectionTracker.SelectedIndexes)}] VALUE=[{string.Join(",",value)}]");
                _selectionTracker.SelectedIndexes = value;
                // System.Diagnostics.Debug.WriteLine($"SegmentedContro[{_instance}] SETTING SELECTED INDEXES : AFTER =[{string.Join(",",_selectionTracker.SelectedIndexes)}]");
            }
        }
        #endregion

        #region SelectedItems
        /// <summary>
        /// List of selected labels
        /// </summary>
        public List<string> SelectedLabels
        {
            get => _selectionTracker.SelectedItems;
            set => _selectionTracker.SelectedItems = value;
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
                control._selectionTracker.SelectionMode = (SelectionMode)args.NewValue;
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
        private readonly List<TextBlock> _textBlocks = [];
        private readonly List<Rectangle> _separators = [];
        private readonly List<Rectangle> _backgrounds = [];
        private readonly CollectionSelectionTracker<string> _selectionTracker = new();
        private readonly TextBlock _testTextBlock = new TextBlock().FontSize(16);
        private readonly Grid _grid = new();

        private static int _instances;
        internal readonly int _instance = _instances++;
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SegmentedControl()
        {
            Content = _grid;

            _testTextBlock
                .WBind(TextBlock.MarginProperty, this, PaddingProperty);

            SizeChanged += SegmentedControl_SizeChanged;
            BorderBrush = SystemToggleButtonBrushes.Border;
            CornerRadius = new CornerRadius(4);
            VerticalAlignment = VerticalAlignment.Center;

            Loaded += OnLoaded;

            Labels = new ObservableCollection<string>();

            _selectionTracker.CollectionChanged += OnSelectionTracker_CollectionChanged;
            _selectionTracker.SelectionChanged += OnSelectionTracker_SelectionChanged;

#if !HAS_UNO
            RegisterPropertyChangedCallback(UserControl.BorderThicknessProperty, OnBorderThicknessChanged);
#endif
            RegisterPropertyChangedCallback(UserControl.IsEnabledProperty, OnIsEnabledChanged);
            RegisterPropertyChangedCallback(UserControl.VisibilityProperty, OnSegmentControlVisibilityChanged);

            _grid.WBind(Grid.CornerRadiusProperty, this, CornerRadiusProperty);
            _grid.WBind(Grid.BorderBrushProperty, this, BorderBrushProperty);
            _grid.WBind(Grid.BorderThicknessProperty, this, BorderThicknessProperty);
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
                _selectionTracker.Clear();
                return this;
            }
            
            if (index < 0 || index >= Labels.Count) return this;

            //SelectLabel(Labels[index]);
            if (!_selectionTracker.SelectedIndexes.Contains(index))
                _selectionTracker.SelectIndex(index);

            return this;
        }

        public SegmentedControl DeselectIndex(int index)
        {
            if (index < 0 || index >= Labels.Count) return this;

            //DeselectLabel(Labels[index]);
            if (_selectionTracker.SelectedIndexes.Contains(index))
                _selectionTracker.UnselectIndex(index);

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
            if (_selectionTracker.SelectedIndexes.Contains(index))
                _selectionTracker.UnselectIndex(index);
            else
                _selectionTracker.SelectIndex(index);
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
            if (index < 0 || index >= Labels.Count || index >= _textBlocks.Count || index >= _backgrounds.Count)
                return;

            if (_tapProcessing == index)
                return;

            var selected = SelectedIndexes.Contains(index);
            var nextSelected = SelectedIndexes.Contains(index + 1);

            
            // if (_instance == 13)
            //System.Diagnostics.Debug.WriteLine(
            //    $"\t UpdateElementColors[{_instance}]({index}:{Labels[index]}) Selected:{selected} SelectedIndexes=[{string.Join(",", SelectedIndexes)}]");
            
            
            var background = _backgrounds[index];
            background.Fill = selected
                ? BorderBrush.AsGesterableEnabled(IsEnabled) 
                : SystemToggleButtonBrushes.Background.AsGesterableEnabled(IsEnabled);

            var textBlock = _textBlocks[index];
            textBlock.Foreground = selected
                ? SystemToggleButtonBrushes.CheckedForeground.AsGesterableEnabled(IsEnabled)
                : Foreground; // SystemToggleButtonBrushes.Foreground.AsGesterableEnabled(IsEnabled);

            if (index >= _separators.Count)
                return;

            var separator = _separators[index];
            separator.Visible(selected == nextSelected);
            separator.Fill = selected
                ? SystemToggleButtonBrushes.CheckedForeground.AsGesterableEnabled(IsEnabled)
                : BorderBrush.AsGesterableEnabled(IsEnabled);
        }

        private void SetTappedProcessingColors(int index)
        {
            if (index < 0 || index >= _textBlocks.Count)
                return;

            var background = _backgrounds[index];
            background.Fill = SystemToggleButtonBrushes.BackgroundCheckedPressed;
            var textBlock = _textBlocks[index];
            textBlock.Foreground = SystemToggleButtonBrushes.ForegroundCheckedPressed;
        }

        private void SetHoverOverColors(int index)
        {
            if (index < 0 || index >= _textBlocks.Count)
                return;
 
            var selected = SelectedIndexes.Contains(index);
            if (_tapProcessing == index)
                return;
            var background = _backgrounds[index];
            background.Fill = selected
                ? SystemToggleButtonBrushes.CheckedPointerOverBackground.AssureGesturable()
                : SystemButtonBrushes.BackgroundPointerOver.AssureGesturable();
            var textBlock = _textBlocks[index];
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
            _tapProcessing = _selectionTracker.SelectedIndex;
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

            /*
            if (_instance == 13)
            {
                System.Diagnostics.Debug.WriteLine($"=== SegmentedContro[{_instance}] DISPLAY SELECTIONS : ENTER ===");
                System.Diagnostics.Debug.WriteLine(
                    $"    SelectedIndexes:[{string.Join(',', SelectedIndexes)}] SelectedLabels:[{string.Join(',', SelectedLabels)}]");
            }
            */

            for (var i=0; i< Labels.Count;i++)
                UpdateElementColors(i);

            /*
            if (_instance == 13)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"    SelectedIndexes:[{string.Join(',', SelectedIndexes)}] SelectedLabels:[{string.Join(',', SelectedLabels)}]");
                System.Diagnostics.Debug.WriteLine($"=== SegmentedContro[{_instance}] DISPLAY SELECTIONS : EXIT ===");
            }
            */
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!(ActualWidth > 20))
                return;

            IsOverflowed = CalculateOverflow(ActualSize.X);
            UpdateChildren();
        }

        private Size _lastSize = default;
        private void SegmentedControl_SizeChanged(object sender, SizeChangedEventArgs args)
        {
            if (_lastSize == args.NewSize)
                return;
            
            _lastSize = args.NewSize;

            if (!IsLoaded)
                return;

            IsOverflowed = CalculateOverflow(ActualSize.X);
            UpdateChildren();
        }
        
        private void OnSegmentControlVisibilityChanged(DependencyObject sender, DependencyProperty dp)
        {
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
            => _textBlocks.Add(new TextBlock()
                .WBind(TextBlock.MarginProperty, this, PaddingProperty)
                .Foreground(SystemToggleButtonBrushes.Foreground)
                .Center()
                .FontSize(16)
                .Column(_textBlocks.Count)
                .AddTappedHandler(OnSegmentTapped)
                .AddPointerEnteredHandler(OnSegmentPointerEntered)
                .AddPointerExitedHandler(OnSegmentPointerExited)
                .AddPointerCanceledHandler(OnSegmentPointerExited)
                .AddPointerCaptureLostHandler(OnSegmentPointerExited)
                );

        private void AddNewSeparator()
            => _separators.Add(new Rectangle()
                .Margin(0,5)
                .Opacity(0.5)
                .Width(1)
                .StretchVertical().CenterHorizontal()
                .ColumnSpan(2).Column(_separators.Count)
                .WBind(Shape.FillProperty, this, BorderBrushProperty)
                );

        private void AddNewBackground()
            => _backgrounds.Add(new Rectangle()
                .Fill(SystemToggleButtonBrushes.Background)
                .Margin(-1)
                .Stretch()
                .Column(_backgrounds.Count)
                .AddTappedHandler(OnSegmentTapped)
                .AddPointerEnteredHandler(OnSegmentPointerEntered)
                .AddPointerExitedHandler(OnSegmentPointerExited)
                .AddPointerCanceledHandler(OnSegmentPointerExited)
                .AddPointerCaptureLostHandler(OnSegmentPointerExited)
                // TODO: Add Tap Handler
                );

        private void AddNewColumn()
            => _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        private void RemoveLastColumn()
            => _grid.ColumnDefinitions.RemoveAt(_grid.ColumnDefinitions.Count - 1);
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
            //System.Diagnostics.Debug.WriteLine($"SegmentedControl.UpdateChildren  : ENTER Labels=[{string.Join(",",Labels)}]  SELECTEDINDEXES=[{string.Join(",",SelectedIndexes)}] SelectedLabel=[{SelectedLabel}]");
            var columns = Labels.Count;

            while (columns > _grid.ColumnDefinitions.Count)
                AddNewColumn();
            while (columns < _grid.ColumnDefinitions.Count)
                RemoveLastColumn();

            if (IsOverflowed)
                _grid.Children.Clear();
            else
            {
                while (_textBlocks.Count < columns)
                    AddNewLabel();
                while (_separators.Count < columns - 1)
                    AddNewSeparator();
                while (_backgrounds.Count < columns)
                    AddNewBackground();

                for (var i = 0; i < Labels.Count; i++)
                {
                    _textBlocks[i].Text(Labels[i]);

                    if (!_grid.Children.Contains(_textBlocks[i]))
                        _grid.Children.Add(_textBlocks[i]);
                    if (i < columns - 1 && !_grid.Children.Contains(_separators[i]))
                        _grid.Children.Add(_separators[i]);
                    if (!_grid.Children.Contains(_backgrounds[i]))
                        _grid.Children.Insert(0, _backgrounds[i]);
                }

                var remove = _grid.Children.Where(c => (int)c.GetValue(Grid.ColumnProperty) >= columns);
                _grid.Children.RemoveRange(remove);

                remove = _grid.Children.Where(c => (int)c.GetValue(Grid.ColumnSpanProperty) > 1 && (int)c.GetValue(Grid.ColumnProperty) >= columns - 1);
                _grid.Children.RemoveRange(remove);

                DisplaySelections();
            }

            UpdateBorder();
            //System.Diagnostics.Debug.WriteLine($"SegmentedControl.UpdateChildren  : EXIT  Labels=[{string.Join(",",Labels)}]  SELECTEDINDEXES=[{string.Join(",",SelectedIndexes)}] SelectedLabel=[{SelectedLabel}]");
        }

        private void OnBorderThicknessChanged(DependencyObject sender, DependencyProperty dp)
        {
            if (IsLoaded && ActualWidth > 50)
                CalculateOverflow(ActualWidth);
        }


        #endregion
    }


    
}


