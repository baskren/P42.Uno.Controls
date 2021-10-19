using P42.Uno.Markup;
using P42.Utils.Uno;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace P42.Uno.Controls
{
    [Windows.UI.Xaml.Data.Bindable]
    [System.ComponentModel.Bindable(System.ComponentModel.BindableSupport.Yes)]
    public partial class SegmentedControl : ContentControl
    {
        #region Properties

        #region Segments Property
        public static readonly DependencyProperty SegmentsProperty = DependencyProperty.Register(
            nameof(Segments),
            typeof(IList<Segment>),
            typeof(SegmentedControl),
            new PropertyMetadata(default(IList<Segment>))
        );
        public IList<Segment> Segments
        {
            get => (IList<Segment>)GetValue(SegmentsProperty);
            set => SetValue(SegmentsProperty, value);
        }
        #endregion Segments Property


        #region Orientation Property
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            nameof(Orientation),
            typeof(Orientation),
            typeof(SegmentedControl),
            new PropertyMetadata(Orientation.Horizontal)
        );
        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }
        #endregion Orientation Property


        #region SegmentContentOrientation Property
        public static readonly DependencyProperty SegmentContentOrientationProperty = DependencyProperty.Register(
            nameof(SegmentContentOrientation),
            typeof(Orientation),
            typeof(SegmentedControl),
            new PropertyMetadata(Orientation.Horizontal, new PropertyChangedCallback(OnSegmentContentOrientationChanged))
        );
        protected static void OnSegmentContentOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SegmentedControl SegmentedControl)
            {
                /*
                foreach (var segment in SegmentedControl.Buttons)
                    segment._stackPanel.Orientation = SegmentedControl.SegmentContentOrientation;
                */
            }
        }
        public Orientation SegmentContentOrientation
        {
            get => (Orientation)GetValue(SegmentContentOrientationProperty);
            set => SetValue(SegmentContentOrientationProperty, value);
        }
        #endregion SegmentContentOrientation Property


        #region SelectionMode Property
        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(
            nameof(SelectionMode),
            typeof(SelectionMode),
            typeof(SegmentedControl),
            new PropertyMetadata(default(SelectionMode), new PropertyChangedCallback(OnSelectionModeChanged))
        );
        protected static void OnSelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SegmentedControl SegmentedControl)
            {
                if (SegmentedControl.SelectionMode == SelectionMode.None)
                    SegmentedControl.DeselectAll();
                else if (SegmentedControl.SelectionMode == SelectionMode.Radio)
                {
                    var firstFound = false;
                    foreach (var button in SegmentedControl.Segments)
                    {
                        if (firstFound)
                            button.IsChecked = false;
                        else if (button.IsSelected)
                            firstFound = true;
                    }
                }
            }
        }
        public SelectionMode SelectionMode
        {
            get => (SelectionMode)GetValue(SelectionModeProperty);
            set => SetValue(SelectionModeProperty, value);
        }
        #endregion SelectionMode Property


        public bool ExceedsAvailableSpace { get; private set; }

        public List<int> SelectedIndexes
        {
            get
            {
                var result = Segments.Where(s => s.IsSelected).Select(s => s.Index).ToList();
                return result;
            }
        }
        #endregion


        #region Events
        public event EventHandler<Segment> SegmentClicked;
        #endregion


        #region Fields
        ObservableCollection<Segment> _buttons = new System.Collections.ObjectModel.ObservableCollection<Segment>();
        #endregion


        #region Construction
        public SegmentedControl()
        {
            Segments = _buttons;
            Build();
            _buttons.CollectionChanged += OnSegmentsCollectionChanged;

            Background = Colors.Black.WithAlpha(0.01).ToBrush();
        }

        #endregion


        #region Measure
        public Size MeasureX(Size availableSize)
            => MeasureOverride(availableSize);

        protected override Size MeasureOverride(Size availableSize)
        {
            var result =  base.MeasureOverride(availableSize);
            ExceedsAvailableSpace = _panel.ExceedsAvailableSpace;
            return result;
        }
        #endregion


        #region Event Handlers
        internal void OnSegmentClicked(Segment segment)
        {
            if (SelectionMode == SelectionMode.None)
                segment.IsChecked = false;
            else if (SelectionMode == SelectionMode.Radio)
            {
                if (segment.IsSelected)
                {
                    foreach (var otherSegment in Segments)
                        if (segment != otherSegment)
                            otherSegment.IsChecked = false;
                }
                else
                    segment.IsChecked = true;
            }
            SegmentClicked?.Invoke(this, segment);
        }

        private void OnSegmentsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    AddChildren(e.NewStartingIndex, e.NewItems);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RemoveChildren(e.OldItems);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RemoveChildren(e.OldItems);
                    AddChildren(e.NewStartingIndex, e.NewItems);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    RemoveChildren(_panel.Children.ToList());
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        void AddChildren(int startIndex, System.Collections.IList items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var segment = (items[i] as Segment)
                    .BindFont(this)
                    .Stretch()
                    .Bind(ForegroundProperty, this, nameof(Foreground))
                    .Bind(HorizontalContentAlignmentProperty, this, nameof(HorizontalContentAlignment))
                    .Bind(BackgroundProperty, this, nameof(Background))
                    .Bind(VerticalContentAlignmentProperty, this, nameof(VerticalContentAlignment))
                    .Bind(IsTextScaleFactorEnabledProperty, this, nameof(IsTextScaleFactorEnabled))
                    .Bind(ElementSoundModeProperty, this, nameof(ElementSoundMode))
                    .BorderBrush(SystemColors.Accent)
                    ;
                segment.Index = startIndex + i;
                _panel.Children.Insert(segment.Index, segment);
            }
            ReindexSegments();
        }

        void RemoveChildren(System.Collections.IList items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var oldChild = items[i] as Segment;
                oldChild.Index = -1;
                _panel.Children.Remove(oldChild);
            }
            ReindexSegments();
        }

        void ReindexSegments()
        {
            for (int i = 0; i < Segments.Count; i++)
            {
                Segments[i].Index = i;
                if (i == 0)
                {
                    if (i == Segments.Count-1)
                    {
                        Segments[i].BorderThickness(1.5, 1.5, 1.5, 1.5);
                        Segments[i].CornerRadius(CornerRadius.TopLeft, CornerRadius.TopRight, CornerRadius.BottomRight, CornerRadius.BottomLeft);
                    }
                    else
                    {
                        Segments[i].BorderThickness(1.5, 1.5, 0.5, 1.5);
                        Segments[i].CornerRadius(CornerRadius.TopLeft, 0, 0, CornerRadius.BottomLeft);
                    }
                }
                else if (i == Segments.Count-1)
                {
                    Segments[i].BorderThickness(0.5, 1.5, 1.5, 1.5);
                    Segments[i].CornerRadius(0, CornerRadius.TopRight, CornerRadius.BottomRight, 0);
                }
                else
                {
                    Segments[i].BorderThickness(0.5, 1.5);
                    Segments[i].CornerRadius(0);
                }
            }
        }
        #endregion


        #region Support Methods

        public void DeselectAll()
        {
            foreach (var button in Segments)
                button.IsChecked = false;
        }

        #endregion
    }
}
